using Fusion;
using UnityEngine;

public class Player : NetworkBehaviour
{
    private CharacterController _rb;

    public override void Spawned()
    {
        _rb = GetComponent<CharacterController>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            _rb.Move(data.direction * Runner.DeltaTime);
        }
    }
}
