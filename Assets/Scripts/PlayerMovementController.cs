using System.Collections;
using System.Collections.Generic;
using BattleGame.HostSimple;
using Fusion;
using Unity.Netcode.Components;
using UnityEngine;

namespace BattleGame
{
    public class PlayerMovementController : NetworkBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private NetworkRigidbody2D rigid;

        private void Awake()
        {
            rigid = GetComponent<NetworkRigidbody2D>();
        }

        public override void Spawned()
        {

        }

        public override void FixedUpdateNetwork()
        {
            var dir = Vector3.zero;
            if (Runner.TryGetInputForPlayer<PlayerInput>(Object.InputAuthority, out var input))
            {
                Debug.Log("Is Server ?");
                if (input.Buttons.IsSet(PlayerButtons.MoveUp)) dir += Vector3.up;
                if (input.Buttons.IsSet(PlayerButtons.MoveDown)) dir += Vector3.down;
                if (input.Buttons.IsSet(PlayerButtons.MoveRight)) dir += Vector3.right;
                if (input.Buttons.IsSet(PlayerButtons.MoveLeft)) dir += Vector3.left;

                if (dir != Vector3.zero)
                {
                    transform.position += Runner.DeltaTime * speed * dir.normalized;
                }
            }
        }
    }
}
