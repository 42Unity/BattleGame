using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Multiplayer.Samples.Utilities;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace BattleGame.Gameplay.State
{
    [RequireComponent(typeof(NetworkHooks))]
    public class ServerGameplayState : GameState
    {
        public static ServerGameplayState Instance { get; private set; }
        [SerializeField] NetworkHooks netHooks;

        [Tooltip("Make sure this is included in the NetworkManager's list of prefabs!")]
        [SerializeField] private NetworkObject playerPrefab;
        [SerializeField] private ProjectileBehaviour bulletPrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.RegisterComponentInNewPrefab(bulletPrefab, Lifetime.Scoped);
        }

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
            netHooks.OnNetworkSpawnHook += OnNetworkSpawn;
            netHooks.OnNetworkDespawnHook += OnNetworkDespawn;
        }

        private void OnNetworkSpawn()
        {
            if (!NetworkManager.Singleton.IsServer)
            {
                enabled = false;
                return;
            }
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnect;
            NetworkManager.Singleton.SceneManager.OnSceneEvent += OnSceneEvent;
        }

        private void OnSceneEvent(SceneEvent sceneEvent)
        {
            Debug.Log("Scene event received: " + sceneEvent.SceneEventType + " for scene " + sceneEvent.SceneName + " for client " + sceneEvent.ClientId);
            if (sceneEvent.SceneEventType == SceneEventType.LoadComplete)
            {
                Debug.Log("Load event completed for scene " + sceneEvent.SceneName + " for client " + sceneEvent.ClientId);
                SpawnPlayer(sceneEvent.ClientId);
            }
        }

        private void OnNetworkDespawn()
        {
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnect;
            NetworkManager.Singleton.SceneManager.OnSceneEvent -= OnSceneEvent;
        }

        private void OnClientDisconnect(ulong obj)
        {
            Debug.Log("Client disconnected: " + obj);
        }

        void SpawnPlayer(ulong clientId)
        {
            Debug.Log("Spawning player for client " + clientId);
            var player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            player.SpawnWithOwnership(clientId, destroyWithScene: true);
        }
    }
}
