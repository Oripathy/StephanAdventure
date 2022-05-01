using Player.StateMachine;
using UnityEngine;

namespace Player.StateMachine
{
    internal abstract class PlayerState
    {
        private protected PlayerStateMachine _stateMachine;
        private protected PlayerData _data;
        private protected PlayerEntity _player;
        private protected bool _isOnGround;
        private protected string _animationBoolName;
        private protected Vector3 _moveDirection;

        public PlayerState(PlayerStateMachine stateMachine, PlayerData data, PlayerEntity player, string animationBoolName)
        {
            _stateMachine = stateMachine;
            _data = data;
            _player = player;
            _animationBoolName = animationBoolName;
        }

        public virtual void OnEnter()
        {
            _player.Animator.SetBool(_animationBoolName, true);
        }

        public virtual void UpdatePass()
        {
            _isOnGround = CheckGroundCollision();
            _moveDirection = _player.InputHandler.MoveDirection;
        }

        public virtual void FixedUpdatePass()
        {
        
        }

        public virtual void OnExit()
        {
            _player.Animator.SetBool(_animationBoolName, false);
        }

        public abstract PlayerState SetNextState();

        private bool CheckGroundCollision()
        {
            return false;
        }
    }
}