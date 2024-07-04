using UnityEngine;
using Unity.Netcode;

public class NetworkManager : NetworkBehaviour
{
    private int pingCount = 0; // pingCount 값을 저장할 변수

    [Rpc(SendTo.Server)]
    public void PingRpc(int pingCount)
    {
        // 서버에서 클라이언트로 전송
        PongRpc(pingCount, "PONG!");
    }

    [Rpc(SendTo.NotServer)]
    void PongRpc(int pingCount, string message)
    {
        Debug.Log($"Received pong from server for ping {pingCount} and message {message}");
    }

    void Update()
    {
        if (IsClient && Input.GetKeyDown(KeyCode.P))
        {
            pingCount++; // pingCount 값을 증가시킴
            PingRpc(pingCount); // pingCount 값을 인자로 전달
        }
    }
}
