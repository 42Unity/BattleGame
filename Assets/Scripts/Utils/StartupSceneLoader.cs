using BattleGame.Application;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupSceneLoader : MonoBehaviour
{
    private void Awake()
    {
        if (ApplicationController.Instance == null)
        {
            SceneManager.LoadScene("Startup");
        }
    }
}
