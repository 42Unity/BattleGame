using Fusion;
using TMPro;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private Ball _prefabBall;
    [SerializeField] private PhysxBall _prefabPhysxBall;
    [Networked] private TickTimer delay { get; set; }
    [Networked] public bool spawnedProjectile { get; set; }
    private ChangeDetector _changeDetector;
    public Material _material;

    public override void Spawned()
    {
        _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);
        _material = GetComponentInChildren<MeshRenderer>().material;
    }

    public void Update()
    {
        if (Object.HasInputAuthority && Input.GetKeyDown(KeyCode.R))
        {
            RPC_SendMessage("Hey Mete!");
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, HostMode = RpcHostMode.SourceIsHostPlayer)]
    public void RPC_SendMessage(string message, RpcInfo info = default)
    {
        RPC_RelayMessage(message, info.Source);
    }

    private TMP_Text _messages;
    [Rpc(RpcSources.StateAuthority, RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    public void RPC_RelayMessage(string message, PlayerRef messageSource)
    {
        if (_messages == null) _messages = FindObjectOfType<TMP_Text>();
        if (messageSource == Runner.LocalPlayer) message = $"You said: {message}";
        else message = $"Player {messageSource.PlayerId} said: {message}";
        _messages.text = message;
    }

    public override void Render()
    {
        foreach (var change in _changeDetector.DetectChanges(this))
        {
            switch (change)
            {
                case nameof(spawnedProjectile):
                    _material.color = Color.white;
                    break;
            }
        }
        _material.color = Color.Lerp(_material.color, Color.blue, Time.deltaTime);
    }

    private NetworkCharacterController _cc;

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterController>();
    }

    private Vector3 _forward = Vector3.forward;
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            data.direction.Normalize();
            _cc.Move(5 * Runner.DeltaTime * data.direction);
            if (data.direction.sqrMagnitude > 0) _forward = data.direction;
            if (HasStateAuthority && delay.ExpiredOrNotRunning(Runner))
            {
                if (data.buttons.IsSet(NetworkInputData.MOUSEBUTTON0))
                {
                    delay = TickTimer.CreateFromSeconds(Runner, 0.5f);
                    Runner.Spawn(_prefabBall,
                        transform.position + _forward, Quaternion.LookRotation(_forward),
                        Object.InputAuthority, (runner, o) =>
                        {
                            o.GetComponent<Ball>().Init();
                        });
                    spawnedProjectile = !spawnedProjectile;
                }
                else if (data.buttons.IsSet(NetworkInputData.MOUSEBUTTON1))
                {
                    delay = TickTimer.CreateFromSeconds(Runner, 0.5f);
                    Runner.Spawn(_prefabPhysxBall,
                        transform.position + _forward,
                        Quaternion.LookRotation(_forward),
                        Object.InputAuthority, (runner, o) =>
                        {
                            o.GetComponent<PhysxBall>().Init(10 * _forward);
                        });
                    spawnedProjectile = !spawnedProjectile;
                }
            }
        }
    }
}
