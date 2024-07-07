using System.ComponentModel;
using System.Linq;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private NetworkManager net;
    [SerializeField] private TextMeshProUGUI readyStatusText;

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

    public void SetReadyStatus(bool ready)
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
}
