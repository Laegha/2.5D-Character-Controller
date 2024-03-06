using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    bool isFalling = false;
    public PlayerJumpState(PlayerStateMachine newContext, PlayerStateFactory newFactory) : base(newContext, newFactory) 
    {
        InitializeSubState();
        isRootState = true;
    }

    public override void EnterState() 
    {
        Jump();
        //if(goingUp)
            context.PlayerAnimator.Play("Jump");
        context.Grounded = false;
    }

    public override void DefineValues()
    {
        thisStateChangeConditions["canAttack"] = false;
        base.DefineValues();
    }

    public override void UpdateState()
    {
        //logica para cambiar la animacion cuando cae
        if (isFalling)
            return;
        if(context.PlayerRb.velocity.y > 0)
        {
            //context.PlayerAnimator.Play("Fall");
            isFalling = true;
        }
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            SwitchState(factory.Grounded());
    }

    public override void ExitState() { }

    public override void CheckSwitchStates() { }

    public override void InitializeSubState() 
    {
        if (context.Movement == Vector2.zero)
            SetSubState(factory.Idle());
        else if (!context.IsRunning)
            SetSubState(factory.Walk());
        else
            SetSubState(factory.Run());
    }

    void Jump()
    {
        context.PlayerRb.AddForce(Vector3.up * context.JumpForce, ForceMode.Impulse);
        context.JumpPressed = false;
    }
}
