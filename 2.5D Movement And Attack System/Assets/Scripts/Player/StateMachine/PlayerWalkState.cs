using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine newContext, PlayerStateFactory newFactory) : base(newContext, newFactory) { }

    public override void EnterState()
    {
        if (context.Grounded)
            context.PlayerAnimator.Play("Walk");
    }

    public override void DefineValues()
    {
        thisStateChangeConditions["canAttack"] = true;
        base.DefineValues();
    }

    public override void UpdateState() 
    { 
        CheckSwitchStates();

        Vector3 movement = context.transform.TransformDirection(new Vector3(context.Movement.x, 0f, context.Movement.y) * context.WalkSpeed);
        context.PlayerRb.velocity = new Vector3(movement.x, context.PlayerRb.velocity.y, movement.z);
    }

    public override void OnCollisionEnter(Collision collision) { }

    public override void ExitState() { }

    public override void CheckSwitchStates() 
    {
        if (context.Movement == Vector2.zero)
            SwitchState(factory.Idle());

        else if (context.IsRunning)
            SwitchState(factory.Run());

        if (context.StateChangeConditions["canAttack"] && context.IsAttacking)
            SwitchState(factory.NormalAttack());
    }

    public override void InitializeSubState() { }
}
