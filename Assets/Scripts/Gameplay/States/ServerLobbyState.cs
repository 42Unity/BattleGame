using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Multiplayer.Samples.Utilities;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BattleGame.Gameplay.State
{
    [RequireComponent(typeof(NetworkHooks))]
    public class ServerLobbyState : GameState
    {
        [SerializeField] private NetworkHooks netHooks;
        [SerializeField] private NetworkReadyState networkReadyState;

        protected override void Awake()
        {
            base.Awake();
            if (netHooks == null) netHooks = GetComponent<NetworkHooks>();
            if (networkReadyState == null) networkReadyState = GetComponent<NetworkReadyState>();
            netHooks.OnNetworkSpawnHook += OnNetworkSpawn;
            netHooks.OnNetworkDespawnHook += OnNetworkDespawn;
        }

        public void OnNetworkSpawn()
        {
            if (!NetworkManager.Singleton.IsServer)
            {
                enabled = false;
            }
            else
            {
                networkReadyState.OnClientReady += OnClientReady;
                NetworkManager.Singleton.SceneManager.OnSceneEvent += OnSceneEvent;
            }
        }

        private void OnSceneEvent(SceneEvent sceneEvent)
        {
            if (sceneEvent.SceneEventType == SceneEventType.LoadComplete)
            {
                networkReadyState.LobbyPlayers.Add(new NetworkReadyState.LobbyPlayerState()
                {
                    clientId = sceneEvent.ClientId,
                    ready = false,
                });
            }
        }

        public void OnNetworkDespawn()
        {
            networkReadyState.OnClientReady -= OnClientReady;
            NetworkManager.Singleton.SceneManager.OnSceneEvent -= OnSceneEvent;
        }

        public void OnClientReady(ulong clientId, bool ready)
        {
            var index = FindPlayerIndex(clientId);
            if (index == -1)
            {
                Debug.LogError($"ServerLobbyState: OnClientReady: Player not found: {clientId}");
                return;
            }
            networkReadyState.LobbyPlayers[index] = new NetworkReadyState.LobbyPlayerState()
            {
                clientId = clientId,
                ready = ready,
            };
            if (IsAllReady() && NetworkManager.Singleton.IsServer)
            {
                NetworkManager.Singleton.SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
            }
        }

        private int FindPlayerIndex(ulong clientId)
        {
            for (int i = 0; i < networkReadyState.LobbyPlayers.Count; i++)
            {
                if (networkReadyState.LobbyPlayers[i].clientId == clientId)
                {
                    return i;
                }
            }
            return -1;
        }

        private bool IsAllReady()
        {
            foreach (var player in networkReadyState.LobbyPlayers)
            {
                if (!player.ready) return false;
            }
            return true;
        }
    }
}
