using UnityEngine;

public class PlayerStateFactory
{
    PlayerStateMachine context;

    public PlayerStateFactory(PlayerStateMachine newContext)
    {
        context = newContext;
    }

    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(context, this);
    }

    public PlayerBaseState Walk()
    {
        return new PlayerWalkState(context, this);
    }

    public PlayerBaseState Run()
    {
        return new PlayerRunState(context, this);
    }

    public PlayerBaseState Jump()
    {
        return new PlayerJumpState(context, this);
    }

    public PlayerBaseState Grounded()
    {
        return new PlayerGroundedState(context, this);
    }

    public PlayerBaseState Dodge()
    {
        return new PlayerDodgeState(context, this);
    }

    public PlayerBaseState NormalAttack()
    {
        return new PlayerAttackState(context, this);
    }

    public PlayerBaseState PostAttack ()
    {
        return new PlayerPostAttackState(context, this);
    }
}
