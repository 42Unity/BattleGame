using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

[RequireComponent(typeof(NetworkObject))]
public class ProjectileBehaviour : NetworkBehaviour
{
    private void Start()
    {
        if (IsServer)
        {
            Destroy(gameObject, 5f);
        }
    }
    private void Update()
    {
        if (IsServer)
        {
            transform.Translate(Vector3.up * 10f * Time.deltaTime);
        }
    }
}
