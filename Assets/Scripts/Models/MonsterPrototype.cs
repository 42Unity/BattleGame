using UnityEngine;

namespace BattleGame.Prototype
{
    [CreateAssetMenu(fileName = "MonsterPrototype", menuName = "BattleGame/MonsterPrototype")]
    public class MonsterPrototype : CharacterPrototype
    {
        public float traceDistance = 1f;
    }
}
