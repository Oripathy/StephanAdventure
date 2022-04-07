using UnityEngine;

internal class SpheroBotMoveState : SpheroBotState
{
    public SpheroBotMoveState(SpheroBotStateMachine stateMachine, SpheroBotData data, SpheroBot spheroBot,
        Transform target, string animationBoolName) : base(stateMachine, data, spheroBot, target, animationBoolName)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _spheroBot.NavMeshAgent.speed = 3f;
    }

    public override void FixedUpdatePass()
    {
        base.FixedUpdatePass();
        _spheroBot.NavMeshAgent.destination = _target.position;
    }

    public override SpheroBotState SetNextState()
    {
        if (_distanceToTarget <= 1.5f) // attack range)
            return _stateMachine.BounceAttack;

        if (_distanceToTarget >= 10f)
            return _stateMachine.RollState;
        
        return this;
    }
}