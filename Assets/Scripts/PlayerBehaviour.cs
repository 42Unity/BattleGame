using BattleGame.Model;

namespace BattleGame
{
    public class PlayerBehaviour : CharacterBehaviour
    {
        public Player Player
        {
            get => Character as Player;
            set => Character = value;
        }
    }
}
