using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

public class NetworkModePopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ipText;

    [Inject] private readonly NetworkManager net;

    public void StartHost()
    {
        net.StartHost();
    }

    public void StartClient()
    {
        net.StartClient();
    }
}