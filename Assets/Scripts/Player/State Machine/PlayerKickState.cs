using System.Collections;
using UnityEngine;

internal class PlayerKickState : PlayerState
{
    private bool _kickDone;
    
    public PlayerKickState(PlayerStateMachine stateMachine, PlayerData data, Player player, string animationBoolName) :
        base(stateMachine, data, player, animationBoolName)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _kickDone = false;
        _player.ExecuteCoroutine(Kick());
    }

    public override PlayerState SetNextState()
    {
        if (_kickDone)
            return _stateMachine.IdleState;

        return this;
    }

    private IEnumerator Kick()
    {
        yield return new WaitForSeconds(0.5f);
        var enemiesInRange = Physics.OverlapSphere(_player.RightFoot.position, 0.2f, _player.EnemyLayer);

        foreach (var enemy in enemiesInRange)
        {
            if (enemy.TryGetComponent<EnemyHealth>(out var enemyHealth))
                enemyHealth.TakeDamage(5f);
        }

        yield return new WaitForSeconds(0.5f);
        _kickDone = true;
    }
}