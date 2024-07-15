using BattleGame.Prototype;
using UnityEngine;

namespace BattleGame.Model
{
    public class Monster : Character
    {
        public MonsterPrototype Prototype { get; set; }
        public float TraceDistance { get; set; }
        public Vector3 StartPosition { get; set; }
        public State CurrentState { get; set; }
        public Character TraceTarget { get; set; }

        public static Monster Create(MonsterPrototype prototype)
        {
            if (prototype == null) return null;
            return new Monster
            {
                Prototype = prototype,

                // character properties
                Position = default,
                AttackType = prototype.attackType,
                AttackDamage = prototype.attackDamage,
                AttackSpeed = prototype.attackSpeed,
                AttackRange = prototype.attackRange,
                SightRange = prototype.sightRange,
                MovementSpeed = prototype.movementSpeed,
                MaxHealth = prototype.health,
                CurrentHealth = prototype.health,
                HealthRegen = prototype.healthRegen,

                // monster properties
                TraceDistance = prototype.traceDistance,
            };
        }

        public enum State
        {
            Idle,
            Trace,
            Back,
            Dead
        }
    }
}
