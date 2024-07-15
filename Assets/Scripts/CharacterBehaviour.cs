using System;
using BattleGame.Model;
using Fusion;

namespace BattleGame
{
    public abstract class CharacterBehaviour : NetworkBehaviour
    {
        public Character Character { get; set; }

        protected void SpawnComplete()
        {
            OnSpawned?.Invoke(Character);
        }

        public event Action<Character> OnSpawned;
    }
}
