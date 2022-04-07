using System.Collections;
using UnityEngine;

internal class SpheroBotBounceAttack : SpheroBotState
{
    private bool _isAttackDone;
    
    public SpheroBotBounceAttack(SpheroBotStateMachine stateMachine, SpheroBotData data, SpheroBot spheroBot,
        Transform target, string animationBoolName) : base(stateMachine, data, spheroBot, target, animationBoolName)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _isAttackDone = false;
        _spheroBot.SetVelocity(Vector3.zero);
        _spheroBot.StartCoroutine(BounceAttack());
    }

    public override SpheroBotState SetNextState()
    {
        if (_isAttackDone)
            return _stateMachine.IdleState;

        return this;
    }

    private IEnumerator BounceAttack()
    {
        yield return new WaitForSeconds(0.8f);
        //Cast circle wave
        yield return new WaitForSeconds(3.6f);
        _isAttackDone = true;
    }
}