using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using VContainer;

public class ClientInputSender : NetworkBehaviour
{
    [SerializeField] private ProjectileBehaviour prefab = null;

    private Vector2 dir = new();

    private bool dirty = false;
    private readonly List<Vector2> move = new(4);

    private void Awake()
    {
        for (int i = 0; i < 4; i++) move.Add(Vector2.zero);
    }

    private void Update()
    {
        if (IsOwner)
        {
            if (Input.GetKeyDown(KeyCode.W)) RecordMove(0, Vector2.up);
            if (Input.GetKeyDown(KeyCode.A)) RecordMove(1, Vector2.left);
            if (Input.GetKeyDown(KeyCode.S)) RecordMove(2, Vector2.down);
            if (Input.GetKeyDown(KeyCode.D)) RecordMove(3, Vector2.right);
            if (Input.GetKeyUp(KeyCode.W)) RecordMove(0, Vector2.zero);
            if (Input.GetKeyUp(KeyCode.A)) RecordMove(1, Vector2.zero);
            if (Input.GetKeyUp(KeyCode.S)) RecordMove(2, Vector2.zero);
            if (Input.GetKeyUp(KeyCode.D)) RecordMove(3, Vector2.zero);
            if (dirty)
            {
                dirty = false;
                ServerSendInputRpc(move.Aggregate((acc, cur) => acc + cur));
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ServerShootRpc();
            }
        }
        if (IsServer)
        {
            transform.position = transform.position + ToVector3(dir * Time.deltaTime);
        }
    }

    public void RecordMove(int index, Vector2 value)
    {
        dirty = true;
        move[index] = value;
    }

    [Rpc(SendTo.Server)]
    public void ServerSendInputRpc(Vector2 dir)
    {
        this.dir = dir == default ? default : dir.normalized;
    }

    [Rpc(SendTo.Server)]
    public void ServerShootRpc()
    {
        var projectile = Instantiate(prefab, transform.position, Quaternion.identity);
        projectile.NetworkObject.SpawnAsPlayerObject(OwnerClientId);
    }

    private Vector3 ToVector3(Vector2 v)
    {
        return new Vector3(v.x, v.y, 0);
    }
}
