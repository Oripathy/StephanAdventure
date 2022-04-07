using UnityEngine;

internal class SpheroBotRollState : SpheroBotState
{
    public SpheroBotRollState(SpheroBotStateMachine stateMachine, SpheroBotData data, SpheroBot spheroBot,
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
        if (Vector3.Distance(_target.position, _spheroBot.transform.position) <= 1.5f)
            return _stateMachine.AttackState;
        
        return this;
    }
}
//TODO : Write some logic and test Bot