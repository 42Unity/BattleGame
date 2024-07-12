using Fusion;
using UnityEngine;

[System.Flags]
public enum InputButton
{
    LEFT = 1 << 0,
    RIGHT = 1 << 1,
    DOWN = 1 << 3,
    UP = 1 << 4,
}

public struct InputData : INetworkInput
{
    public NetworkButtons Buttons;

    public Vector2 GetMovementDirection()
    {
        var dir = Vector2.zero;
        if (Buttons.IsSet(InputButton.LEFT)) dir.x -= 1;
        if (Buttons.IsSet(InputButton.RIGHT)) dir.x += 1;
        if (Buttons.IsSet(InputButton.DOWN)) dir.y -= 1;
        if (Buttons.IsSet(InputButton.UP)) dir.y += 1;
        return dir.normalized;
    }
}
