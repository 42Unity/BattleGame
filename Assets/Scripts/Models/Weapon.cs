using BattleGame.Prototype;

namespace BattleGame.Model
{
    public class Weapon : Object
    {
        public WeaponPrototype Prototype { get; set; }
        public float AddAttackDamage { get; set; }
        public float AddAttackSpeed { get; set; } // attacks per second
        public float AddAttackRange { get; set; }

        public static Weapon Create(WeaponPrototype prototype)
        {
            if (prototype == null) return null;
            return new Weapon
            {
                Prototype = prototype,
                AddAttackDamage = prototype.addAttackDamage,
                AddAttackSpeed = prototype.addAttackSpeed,
                AddAttackRange = prototype.addAttackRange,
            };
        }
    }
}
