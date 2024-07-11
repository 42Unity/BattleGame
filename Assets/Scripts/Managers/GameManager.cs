using System.ComponentModel;
using System.Linq;
using TMPro;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(NetworkObject))]
public class GameManager : NetworkBehaviour
{
    [SerializeField] private NetworkManager net;
    [SerializeField] private TextMeshProUGUI readyStatusText;
    [SerializeField] private TextMeshProUGUI debugText;
    [SerializeField] private RectTransform selectModePanel;
    [SerializeField] private RectTransform readyPanel;
    private bool myReadyStatus;

    [Rpc(SendTo.Server)]
    private void ReadyRpc()
    {
        DebugLog("server: recive ready command");
        if (IsAllClientReady())
        {
            StartGameRpc();
        }
    }

    private bool IsAllClientReady()
    {
        return false;
    }

    [Rpc(SendTo.Server)]
    private void CancelReadyRpc()
    {
        DebugLog("server: recive cancel ready command");
    }

    [Rpc(SendTo.Everyone)]
    private void StartGameRpc()
    {
        DebugLog("server -> client: start game");
    }

    public void PressReadyButton()
    {
        if (myReadyStatus)
        {
            DebugLog("client: cancel ready");
            CancelReadyRpc();
        }
        else
        {
            DebugLog("client: ready");
            ReadyRpc();
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
