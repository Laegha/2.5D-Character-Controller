using UnityEngine;

public class PlayerPostAttackState : PlayerBaseState
{
    Rotator rotator;
    public PlayerPostAttackState(PlayerStateMachine newContext, PlayerStateFactory newFactory) : base(newContext, newFactory) { }

    public override void EnterState() 
    {
        if (context.Grounded)
            context.PlayerAnimator.Play("Idle");
        rotator = new Rotator(context.PlayerAttackController.playerGFX.localRotation.eulerAngles.y, 0, context.PlayerAttackController.rotationTime, context.PlayerAttackController.playerGFX);
        context.PlayerAttackController.StartComboResetCooldown();
    }

    public override void DefineValues()
    {
        thisStateChangeConditions["canAttack"] = true;
        base.DefineValues();
    }

    public override void UpdateState() 
    {
        if (rotator != null)
        {
            if (rotator.hasFinished)
            {
                rotator = null;
                return;
            }

            rotator.RotateTo();
        }
        CheckSwitchStates();
    }

    public override void OnCollisionEnter(Collision collision) { }

    public override void ExitState() { }

    public override void CheckSwitchStates() 
    {
        if (context.Movement == Vector2.zero && rotator.hasFinished)
            SwitchState(factory.Idle());

        if (context.Movement != Vector2.zero && !context.IsRunning && rotator.hasFinished)
            SwitchState(factory.Walk());
        
        else if(context.Movement != Vector2.zero && context.IsRunning && rotator.hasFinished)
            SwitchState(factory.Run());
    }

    public override void InitializeSubState() { }
}
