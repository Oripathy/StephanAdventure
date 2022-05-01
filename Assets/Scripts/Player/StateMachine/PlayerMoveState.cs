using Player.StateMachine;
using UnityEngine;

namespace Player.StateMachine
{
    internal class PlayerMoveState : PlayerState
    {
        private Vector3 _moveDirectionNew;
        public PlayerMoveState(PlayerStateMachine stateMachine, PlayerData data, PlayerEntity player, string animationBoolName) :
            base(stateMachine, data, player, animationBoolName)
        {
        }

        public override void FixedUpdatePass()
        {
            base.FixedUpdatePass();
            _moveDirectionNew = _player.CalculateMoveDirectionOnSurface();
            _player.SetVelocity(_moveDirectionNew * 5f);
            _player.Rotate();
        }

        public override PlayerState SetNextState()
        {
            if (_moveDirectionNew.magnitude <= 0.1f)
                return _stateMachine.IdleState;

            return this;
        }
    }
}