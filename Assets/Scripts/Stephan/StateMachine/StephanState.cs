using UnityEngine;

namespace Stephan.StateMachine
{
    internal abstract class StephanState
    {
        private protected StephanStateMachine _stateMachine;
        private protected StephanData _data;
        private protected StephanEntity _stephan;
        private protected Transform _player;
        private protected string _animationBoolName;
        private protected float _distanceToPlayer;

        public StephanState(StephanStateMachine stateMachine, StephanData data, StephanEntity stephan, Transform player,
            string animationBoolName)
        {
            _stateMachine = stateMachine;
            _data = data;
            _stephan = stephan;
            _player = player;
            _animationBoolName = animationBoolName;
        }

        public virtual void OnEnter()
        {
            _stephan.Animator.SetBool(_animationBoolName, true);
        }

        public virtual void UpdatePass()
        {
            CalculateDistanceToPlayer();
        }

        public virtual void FixedUpdatePass()
        {
        
        }

        public virtual void OnExit()
        {
            _stephan.Animator.SetBool(_animationBoolName, false);
        }

        public abstract StephanState SetNextState();

        private protected void CalculateDistanceToPlayer() => _distanceToPlayer =
            Vector3.Distance(_stephan.transform.position, _player.transform.position);
    }
}