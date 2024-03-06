using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine newContext, PlayerStateFactory newFactory) : base(newContext, newFactory) 
    {
        context.Grounded = true;
        InitializeSubState();
        isRootState = true;
    }
    public override void EnterState() { }

    public override void DefineValues()
    {
        thisStateChangeConditions["canAttack"] = true;
        base.DefineValues();
    }


    public override void UpdateState() 
    { 
        CheckSwitchStates();
    }

    public override void OnCollisionEnter(Collision collision) { }

    public override void ExitState() { }

    public override void CheckSwitchStates() 
    {
        if (context.JumpPressed)
            SwitchState(factory.Jump());
    }

    public override void InitializeSubState() 
    {
        if(context.Movement == Vector2.zero)
            SetSubState(factory.Idle());
        else if(!context.IsRunning)
            SetSubState(factory.Walk());
        else 
            SetSubState(factory.Run());
    }
}
