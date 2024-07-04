using System.ComponentModel;
using System.Linq;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private NetworkManager net;
    [SerializeField] private TextMeshProUGUI readyStatusText;
    [SerializeField] private TextMeshProUGUI debugText;
    [SerializeField] private RectTransform selectModePanel;
    [SerializeField] private RectTransform readyPanel;
    private bool myReadyStatus;

    [Rpc(SendTo.Server)]
    private void Ready()
    {
        DebugLog("server: recive ready command");
        if (IsAllClientReady())
        {
            StartGame();
        }
    }

    private bool IsAllClientReady()
    {
        return false;
    }

    [Rpc(SendTo.Server)]
    private void CancelReady()
    {
        DebugLog("server: recive cancel ready command");
    }

    [Rpc(SendTo.Everyone)]
    private void StartGame()
    {
        DebugLog("server -> client: start game");
    }

    public void PressReadyButton()
    {
        if (myReadyStatus)
        {
            DebugLog("client: cancel ready");
            CancelReady();
        }
        else
        {
            DebugLog("client: ready");
            Ready();
        }
        SetReadyStatus(!myReadyStatus);
    }

    private void SetReadyStatus(bool ready)
    {
        myReadyStatus = ready;
        if (ready)
        {
            readyStatusText.text = "Ready!";
            readyStatusText.color = Color.green;
        }
        else
        {
            readyStatusText.text = "Not Ready...";
            readyStatusText.color = Color.red;
        }
    }

    private void DebugLog(string text)
    {
        debugText.text += text + "\n";
        Debug.Log(text);
    }

    public void StartServer()
    {
        net.StartServer();
        Destroy(selectModePanel.gameObject);
        Destroy(readyPanel.gameObject);
        DebugLog("start server");
    }
    public void StartClient()
    {
        net.StartClient();
        Destroy(selectModePanel.gameObject);
        SetReadyStatus(false);
        DebugLog("start client");
    }
    public void StartHost()
    {
        net.StartHost();
        Destroy(selectModePanel.gameObject);
        SetReadyStatus(false);
        DebugLog("start host");
    }
}
