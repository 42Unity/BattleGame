using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(NetworkObject))]
public class PlayerBehaviour : NetworkBehaviour
{
    private void Awake()
    {
        Debug.Log("adsf");
    }
}
