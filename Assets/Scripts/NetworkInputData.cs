using Fusion;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    public const byte KEYUP = 1;
    public const byte KEYDOWN = 2;
    public const byte KEYLEFT = 3;
    public const byte KEYRIGHT = 4;

    public NetworkButtons buttons;
    public Vector2 direction;
}
