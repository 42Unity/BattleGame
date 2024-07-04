using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private NetworkManager net;

    public void StartServer()
    {
        net.StartServer();
    } 
    public void StartHost()
    {
        net.StartHost();
    } 
    public void StartClient()
    {
        net.StartClient();
    } 
}
