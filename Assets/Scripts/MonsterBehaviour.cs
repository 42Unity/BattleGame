using BattleGame.Model;
using UnityEngine;

namespace BattleGame
{
    public class MonsterBehaviour : CharacterBehaviour
    {
        public Monster Monster
        {
            get => Character as Monster;
            set => Character = value;
        }
    }
}
