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
        protected Character Character => characterBehaviour.Character;
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
        private void SetPosition(Vector2 position)
        {
            Character.Position = position;
            Position = position;
        }

        public void Move(Vector2 direction)
        {
            var currentPos = rigid.Rigidbody.position;
            var velocity = direction.normalized * Character.MovementSpeed;
            var newPos = Runner.DeltaTime * velocity + currentPos;
            SetPosition(newPos);
        }

        public override void FixedUpdateNetwork()
        {
            rigid.Rigidbody.position = Position;
        }
    }
}
