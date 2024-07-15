using UnityEngine;

namespace BattleGame.Prototype
{
    [CreateAssetMenu(fileName = "PlayerPrototype", menuName = "BattleGame/PlayerPrototype")]
    public class PlayerPrototype : CharacterPrototype
    {
        public WeaponPrototype weapon = null;
    }
}
