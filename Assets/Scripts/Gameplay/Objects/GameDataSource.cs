using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleGame.Gameplay.Objects
{
    public class GameDataSource : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}
