namespace BattleGame.Model
{
    public enum AttackType
    {
        Melee = 0,
        Ranged = 1,
    }

    public class Character : Object
    {
        public AttackType AttackType { get; set; }
        public float AttackDamage { get; set; }
        public float AttackSpeed { get; set; } // attacks per second
        public float AttackRange { get; set; }
        public float SightRange { get; set; }
        public float MovementSpeed { get; set; }
        public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }
        public float HealthRegen { get; set; } // health per second
    }
}
