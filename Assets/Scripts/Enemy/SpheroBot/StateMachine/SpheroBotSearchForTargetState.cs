using System.Collections;
using UnityEngine;

internal class SpheroBotSearchForTargetState : SpheroBotState
{
    private bool _isSearchDone;
    
    public SpheroBotSearchForTargetState(SpheroBotStateMachine stateMachine, SpheroBotData data, SpheroBot spheroBot,
        Transform target, string animationBoolName) : base(stateMachine, data, spheroBot, target, animationBoolName)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _isSearchDone = false;
        _spheroBot.SetVelocity(Vector3.zero);
        _spheroBot.StartCoroutine(WaitSomeTime());
    }

    public override SpheroBotState SetNextState()
    {
        if (_isSearchDone)
            return _stateMachine.IdleState;
        
        return this;
    }
    
    private IEnumerator WaitSomeTime()
    {
        yield return new WaitForSeconds(5f);
        _isSearchDone = true;
    }
}