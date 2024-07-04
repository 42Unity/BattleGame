using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace test2.Assets.Script
{

    public class MonsterPrototype
    {
        public int Id {get; set;}
        public float Health {get; set;}
    }

    // 멧돼지: 체력 5
    // 다람쥐: 체력 1
    public class Monster
    {
        public MonsterPrototype prototype;
        public float MaxHealth {get; set;}
        public float CurrentHealth {get; set;}
    }

    public static class MonsterFactory
    {
        public static int ID_멧돼지 = 1;
        public static Monster CreateMonster(MonsterPrototype prototype)
        {
            if (prototype.Id == ID_멧돼지)
                return new Monster() {
                    MaxHealth = prototype.Health,
                    CurrentHealth = prototype.Health,
                };
            return null;
        } 
    }








    public class Player
    {
        private Vector3 position;
        public Vector3 Position {
            get => position;
            set{
                position = value;
            }
        }
        public float Power {get; set;}
        public float AttackSpeed {get; set;}
        public float Range{get; set;}
    }
}
