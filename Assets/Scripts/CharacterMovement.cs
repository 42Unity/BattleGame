using BattleGame.Model;
using Fusion;
using Fusion.Addons.Physics;
using UnityEngine;

namespace BattleGame
{
    [RequireComponent(typeof(NetworkRigidbody2D))]
    public abstract class CharacterMovement : NetworkBehaviour
    {
        [SerializeField] private CharacterBehaviour characterBehaviour;
        protected Character Player => characterBehaviour.Character;
        [Networked] private Vector2 Position { get; set; }
        protected NetworkRigidbody2D rigid;

        private void Awake()
        {
            if (characterBehaviour == null)
            {
                characterBehaviour = GetComponent<CharacterBehaviour>();
            }
            rigid = GetComponent<NetworkRigidbody2D>();
        }

        public void Teleport(Vector3 position)
        {
            Player.Position = position;
            Position = position;
        }

        public void Move(Vector2 direction)
        {
            var currentPos = rigid.Rigidbody.position;
            var velocity = direction.normalized * Player.MovementSpeed;
            var newPos = Runner.DeltaTime * velocity + currentPos;
            Teleport(newPos);
        }

        public override void FixedUpdateNetwork()
        {
            rigid.Rigidbody.position = Position;
        }
    }
}
