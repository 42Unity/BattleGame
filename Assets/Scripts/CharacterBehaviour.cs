using BattleGame.Model;
using Fusion;

namespace BattleGame
{
    public abstract class CharacterBehaviour : NetworkBehaviour
    {
        public Character Character { get; set; }
    }
}
