using BattleGame.Model;
using Fusion;

namespace BattleGame
{
    public class PlayerBehaviour : CharacterBehaviour
    {
        public Player Player
        {
            get => Character as Player;
            set => Character = value;
        }

        [Networked] private int PrototypeId { get; set; }

        public override void Spawned()
        {
            base.Spawned();
            if (Runner.IsServer)
            {
                PrototypeId = Player.Prototype.GetId();
            }
            if (Runner.IsClient)
            {
                var prototype = PrototypeId.GetPlayerPrototype();
                Player = Player.Create(prototype);
            }
            SpawnComplete();
        }
    }
}
