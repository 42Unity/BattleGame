using BattleGame.Prototype;
using UnityEngine;

namespace BattleGame
{
    public class ResourceManager : MonoBehaviour
    {
        public static ResourceManager Instance { get; private set; }

        public PlayerPrototype defaultPlayerPrototype;
        public MonsterPrototype defaultMonsterPrototype;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
