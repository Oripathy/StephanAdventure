using UnityEngine;

internal class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerStateMachine stateMachine, PlayerData data, Player player, string animationBoolName) :
        base(stateMachine, data, player, animationBoolName)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _player.SetVelocity(Vector3.zero);
    }

    public override PlayerState SetNextState()
    {
        if (_stateMachine.AttackButtonPressed)
            return _stateMachine.KickState;
        
        if (_moveDirection.magnitude > 0.1f)
            return _stateMachine.MoveState;
        
        return this;
    }
}