
using Fusion;
using Fusion.Addons.Physics;
using UnityEngine;

public class PlayerRigidbodyMovement : NetworkBehaviour
{
    private NetworkRigidbody2D _rb;
    [SerializeField] private float speed = 5;
    [Networked] private Vector2 Position { get; set; }

    private void Awake()
    {
        _rb = GetBehaviour<NetworkRigidbody2D>();
    }

    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority && GetInput<InputData>(out var input))
        {
            var velocity = input.GetMovementDirection() * speed;
            Position = _rb.Rigidbody.position + Runner.DeltaTime * velocity;
        }
        _rb.Rigidbody.position = Position;
    }
}
