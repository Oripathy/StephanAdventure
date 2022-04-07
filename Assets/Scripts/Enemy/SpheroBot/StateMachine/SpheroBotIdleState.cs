using UnityEngine;

internal class SpheroBotIdleState : SpheroBotState
{
    public SpheroBotIdleState(SpheroBotStateMachine stateMachine, SpheroBotData data, SpheroBot spheroBot,
        Transform target, string animationBoolName) : base(stateMachine, data, spheroBot, target, animationBoolName)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _spheroBot.SetVelocity(Vector3.zero);
    }

    public override SpheroBotState SetNextState()
    {
        if (_distanceToTarget <= 5f)
            return _stateMachine.MoveState;
        
        return _stateMachine.RollState;
    }
}