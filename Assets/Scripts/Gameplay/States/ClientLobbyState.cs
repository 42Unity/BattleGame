using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Multiplayer.Samples.Utilities;
using Unity.Netcode;

namespace BattleGame.Gameplay.State
{
    [RequireComponent(typeof(NetworkHooks))]
    public class ClientLobbyState : GameState
    {
        public static ClientLobbyState Instance { get; private set; }

        [SerializeField] NetworkHooks netHooks;

        [SerializeField] NetworkReadyState networkReadyState;

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
            if (netHooks == null) netHooks = GetComponent<NetworkHooks>();
            if (networkReadyState == null) networkReadyState = GetComponent<NetworkReadyState>();
            netHooks.OnNetworkSpawnHook += OnNetworkSpawn;
            netHooks.OnNetworkDespawnHook += OnNetworkDespawn;
        }

        public void OnNetworkSpawn()
        {
        }

        public void OnNetworkDespawn()
        {
        }

        public void Ready()
        {
            networkReadyState.ServerReadyRpc(NetworkManager.Singleton.LocalClientId, true);
        }
    }
}
