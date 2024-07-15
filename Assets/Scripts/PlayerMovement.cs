
using BattleGame;

public class PlayerMovement : CharacterMovement
{
    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority && GetInput<InputData>(out var input))
        {
            Move(input.GetMovementDirection());
        }
        base.FixedUpdateNetwork();
    }
}
