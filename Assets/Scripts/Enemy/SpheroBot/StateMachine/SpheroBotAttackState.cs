using System.Collections;
using UnityEngine;

internal class SpheroBotAttackState : SpheroBotState
{
    private bool _isAttackDone;
    
    public SpheroBotAttackState(SpheroBotStateMachine stateMachine, SpheroBotData data, SpheroBot spheroBot,
        Transform target, string animationBoolName) : base(stateMachine, data, spheroBot, target, animationBoolName)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _isAttackDone = false;
        _spheroBot.SetVelocity(Vector3.zero);
        _spheroBot.StartCoroutine(Attack());
    }

    public override SpheroBotState SetNextState()
    {
        if (_isAttackDone)
            return _stateMachine.SearchForTargetState;

        return this;
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.6f);
        var targetInRange = Physics.OverlapSphere(_spheroBot.Body.position, 0.374f, _spheroBot.PlayerLayer);

        foreach (var target in targetInRange)
        {
            //if (target.TryGetComponent<PlayerHealth>(out var playerHealth))
            // {
            //     playerHealth.TakeDamage;
            // }
        }

        yield return new WaitForSeconds(1.3f);
        _isAttackDone = true;
    }
}