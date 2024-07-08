using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace BattleGame.Gameplay.State
{
    public class NetworkReadyState : NetworkBehaviour
    {
        public event Action<ulong, bool> OnClientReady;
        public NetworkList<LobbyPlayerState> LobbyPlayers { get; private set; }

        private void Awake()
        {
            LobbyPlayers ??= new NetworkList<LobbyPlayerState>();
        }


        [Rpc(SendTo.Server)]
        public void ServerReadyRpc(ulong clientId, bool ready)
        {
            OnClientReady?.Invoke(clientId, ready);
        }

        public struct LobbyPlayerState : INetworkSerializable, IEquatable<LobbyPlayerState>
        {
            public ulong clientId;
            public bool ready;

            public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
            {
                serializer.SerializeValue(ref clientId);
                serializer.SerializeValue(ref ready);
            }

            public readonly bool Equals(LobbyPlayerState other)
            {
                return clientId == other.clientId && ready == other.ready;
            }
        }
    }
}
