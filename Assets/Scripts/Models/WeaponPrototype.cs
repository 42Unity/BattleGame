using BattleGame.Model;
using UnityEngine;

namespace BattleGame.Prototype
{
    [CreateAssetMenu(fileName = "WeaponPrototype", menuName = "BattleGame/WeaponPrototype")]
    public class WeaponPrototype : ScriptableObject
    {
        public float addAttackDamage = 0f;
        public float addAttackSpeed = 0f; // attacks per second
        public float addAttackRange = 0f;
    }
}
