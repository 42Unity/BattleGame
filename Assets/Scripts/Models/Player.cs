using BattleGame.Prototype;

namespace BattleGame.Model
{
    public class Player : Character
    {
        public PlayerPrototype Prototype { get; set; }
        public Weapon Weapon { get; set; }
        public float CurrentExp { get; set; }
        public int Level { get; set; }

        public static Player Create(PlayerPrototype prototype)
        {
            if (prototype == null) return null;
            return new Player
            {
                Prototype = prototype,

                // character properties
                AttackType = prototype.attackType,
                AttackDamage = prototype.attackDamage,
                AttackSpeed = prototype.attackSpeed,
                AttackRange = prototype.attackRange,
                SightRange = prototype.sightRange,
                MovementSpeed = prototype.movementSpeed,
                MaxHealth = prototype.health,
                CurrentHealth = prototype.health,
                HealthRegen = prototype.healthRegen,

                // player properties
                Weapon = Weapon.Create(prototype.weapon),
                CurrentExp = 0,
                Level = 1,
            };
        }
    }
}
