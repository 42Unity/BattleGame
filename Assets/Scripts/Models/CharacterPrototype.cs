using UnityEngine;

namespace BattleGame.Prototype
{
    public enum AttackType
    {
        Melee = 0,
        Ranged = 1,
    }

    public abstract class CharacterPrototype : ScriptableObject
    {
        public AttackType attackType = AttackType.Melee;
        public float attackDamage = 1f;
        public float attackSpeed = 1f; // attacks per second
        public float attackRange = 1f;
        public float sightRange = 1f;
        public float movementSpeed = 1f;
        public float health = 1f;
        public float healthRegen = 1f; // health per second
    }
}
