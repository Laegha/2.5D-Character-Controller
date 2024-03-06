using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    Rotator rotator;
    float animationTimer;
    public PlayerAttackState(PlayerStateMachine newContext, PlayerStateFactory newFactory) : base(newContext, newFactory) { }

    public override void EnterState()
    {
        context.IsAttacking = false;
        context.PlayerAttackController.NormalAttackPerformed();
        rotator = new Rotator(context.PlayerAttackController.playerGFX.localRotation.eulerAngles.y, context.PlayerAttackController.desiredNormalAttackRotation, context.PlayerAttackController.rotationTime, context.PlayerAttackController.playerGFX);
    }

    public override void DefineValues()
    {
        thisStateChangeConditions["canAttack"] = true;
        base.DefineValues();
    }

    public override void UpdateState() 
    {
        animationTimer -= Time.deltaTime;
        CheckSwitchStates();

        if (rotator != null)
        {
            if (rotator.hasFinished)
            {
                rotator = null;
                return;
            }

            rotator.RotateTo();
        }
    }

    public override void OnCollisionEnter(Collision collision) { }

    public override void ExitState() { }

    public override void CheckSwitchStates() 
    {
        //if (animationTimer <= 0) deberia sustituir al animationFinished
        if(context.PlayerAttackController.animationFinished && context.IsAttacking)
        {
            context.PlayerAttackController.animationFinished = false;
            SwitchState(factory.NormalAttack());
        }

        else if(context.PlayerAttackController.animationFinished)
        {
            context.PlayerAttackController.animationFinished = false;
            SwitchState(factory.PostAttack());
        }
    }

    public override void InitializeSubState() { }
}
