using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

namespace BattleGame.HostSimple
{
    enum PlayerButtons
    {
        MoveUp = 0,
        MoveDown = 1,
        MoveRight = 2,
        MoveLeft = 4,
        Dash = 8,
    }

    public struct PlayerInput : INetworkInput
    {
        public Vector3 direction;
        public NetworkButtons Buttons;
    }

    public class LocalInputPoller : MonoBehaviour, INetworkRunnerCallbacks
    {
        public const string MOVE_UP = "MoveUp";
        public const string MOVE_DOWN = "MoveDown";
        public const string MOVE_RIGHT = "MoveRight";
        public const string MOVE_LEFT = "MoveLeft";

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            PlayerInput localInput = new();

            var dir = Vector3.zero;
            if (Input.GetKey(KeyCode.W)) dir += Vector3.up;
            if (Input.GetKey(KeyCode.S)) dir += Vector3.down;
            if (Input.GetKey(KeyCode.D)) dir += Vector3.right;
            if (Input.GetKey(KeyCode.A)) dir += Vector3.left;
            localInput.direction = dir;

            localInput.Buttons.Set(PlayerButtons.MoveUp, Input.GetKey(KeyCode.W));
            localInput.Buttons.Set(PlayerButtons.MoveDown, Input.GetKey(KeyCode.S));
            localInput.Buttons.Set(PlayerButtons.MoveRight, Input.GetKey(KeyCode.D));
            localInput.Buttons.Set(PlayerButtons.MoveLeft, Input.GetKey(KeyCode.A));
            // TODO: dash input

            input.Set(localInput);
        }

        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }
        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
        public void OnConnectedToServer(NetworkRunner runner) { }
        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
        public void OnSceneLoadDone(NetworkRunner runner) { }
        public void OnSceneLoadStart(NetworkRunner runner) { }
    }
}
