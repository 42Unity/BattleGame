using System.Diagnostics;
using BattleGame.Model;
using Fusion;

namespace BattleGame
{
    public class MonsterBehaviour : CharacterBehaviour
    {
        public Monster Monster
        {
            get => Character as Monster;
            set => Character = value;
        }

        [Networked] private int PrototypeId { get; set; }

        public override void Spawned()
        {
            base.Spawned();
            if (Runner.IsServer)
            {
                PrototypeId = Monster.Prototype.GetId();
            }
            if (Runner.IsClient)
            {
                var prototype = PrototypeId.GetMonsterPrototype();
                Monster = Monster.Create(prototype);
            }
            SpawnComplete();
        }

    }
}
