using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    public PlayerDodgeState(PlayerStateMachine newContext, PlayerStateFactory newFactory) : base(newContext, newFactory) { }

    public override void EnterState() { }
    public override void DefineValues()
    {
        thisStateChangeConditions["canAttack"] = false;
        base.DefineValues();
    }

    public override void UpdateState() { }

    public override void OnCollisionEnter(Collision collision) { }

    public override void ExitState() { }

    public override void CheckSwitchStates() { }

    public override void InitializeSubState() { }
}
