using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine newContext, PlayerStateFactory newFactory) : base(newContext, newFactory) { }

    public override void EnterState() 
    {
        if (context.Grounded)
            context.PlayerAnimator.Play("Idle");
    }

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
        if (context.Movement != Vector2.zero && !context.IsRunning)
            SwitchState(factory.Walk());
        
        else if(context.Movement != Vector2.zero && context.IsRunning)
            SwitchState(factory.Run());

        if (context.StateChangeConditions["canAttack"] && context.IsAttacking)
            SwitchState(factory.NormalAttack());
    }

    public override void InitializeSubState() { }
}
