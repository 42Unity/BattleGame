using BattleGame.Prototype;
using ExitGames.Client.Photon.StructWrapping;
using Unity.VisualScripting;
using UnityEngine;

namespace BattleGame
{
    public class ResourceManager : MonoBehaviour
    {
        public static ResourceManager Instance { get; private set; }

        public PlayerPrototype defaultPlayerPrototype;
        public MonsterPrototype defaultMonsterPrototype;
        public WeaponPrototype defaultWeaponPrototype;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public int GetId(PlayerPrototype id)
        {
            return 1;
        }

        public PlayerPrototype GetPlayerPrototype(int id)
        {
            return defaultPlayerPrototype;
        }

        public int GetId(MonsterPrototype id)
        {
            return 2;
        }

        public MonsterPrototype GetMonsterPrototype(int id)
        {
            return defaultMonsterPrototype;
        }

        public int GetId(WeaponPrototype id)
        {
            return 3;
        }

        public WeaponPrototype GetWeaponPrototype(int id)
        {
            return null;
        }
    }

    public static class ResourceManagerExtensions
    {
        public static int GetId(this PlayerPrototype so)
        {
            return ResourceManager.Instance.GetId(so);
        }

        public static PlayerPrototype GetPlayerPrototype(this int resourceManager)
        {
            return ResourceManager.Instance.GetPlayerPrototype(resourceManager);
        }

        public static int GetId(this MonsterPrototype so)
        {
            return ResourceManager.Instance.GetId(so);
        }

        public static MonsterPrototype GetMonsterPrototype(this int resourceManager)
        {
            return ResourceManager.Instance.GetMonsterPrototype(resourceManager);
        }

        public static int GetId(this WeaponPrototype so)
        {
            return ResourceManager.Instance.GetId(so);
        }

        public static WeaponPrototype GetWeaponPrototype(this int resourceManager)
        {
            return ResourceManager.Instance.GetWeaponPrototype(resourceManager);
        }
    }
}
