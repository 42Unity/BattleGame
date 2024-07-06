using System.ComponentModel;
using System.Linq;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    [SerializeField] private NetworkManager net;
    [SerializeField] private TextMeshProUGUI readyStatusText;
    [SerializeField] private RectTransform selectModePanel;
    [SerializeField] private RectTransform readyPanel;
    private bool myReadyStatus;

    [Rpc(SendTo.Server)]
    public void ReadyRpc()
    {
        DebugLog.Print("server: recive ready command");
    }

    [Rpc(SendTo.Server)]
    public void CancelReadyRpc()
    {
        DebugLog.Print("server: recive cancel ready command");
    }

    [Rpc(SendTo.Everyone)]
    public void StartGameRpc()
    {
        DebugLog.Print("server -> client: start game");
    }

    private bool IsAllClientReady()
    {
        return false;
    }

    public void PressReadyButton()
    {
        if (myReadyStatus)
        {
            DebugLog.Print("client: cancel ready");
            CancelReadyRpc();
        }
        else
        {
            DebugLog.Print("client: ready");
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

    public void StartServer()
    {
        net.StartServer();
        Destroy(selectModePanel.gameObject);
        Destroy(readyPanel.gameObject);
        DebugLog.Print("start server");
    }
    public void StartClient()
    {
        net.StartClient();
        Destroy(selectModePanel.gameObject);
        SetReadyStatus(false);
        DebugLog.Print("start client");
    }
    public void StartHost()
    {
        net.StartHost();
        Destroy(selectModePanel.gameObject);
        SetReadyStatus(false);
        DebugLog.Print("start host");
    }
}
