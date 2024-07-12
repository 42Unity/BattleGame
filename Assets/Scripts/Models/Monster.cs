using UnityEngine;

namespace BattleGame.Model
{
    public class Monster : Character
    {
        public Vector3 StartPosition { get; set; }
        public float TraceDistance { get; set; }
        public bool IsDead { get; set; }
    }
}
