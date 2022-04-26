using UnityEngine;

namespace Stephan.StateMachine
{
    internal class StephanFollowPlayerState : StephanState
    {
        public StephanFollowPlayerState(StephanStateMachine stateMachine, StephanData data, StephanEntity stephan,
            Transform player, string animationBoolName) : base(stateMachine, data, stephan, player, animationBoolName)
        {
        }

        public override void FixedUpdatePass()
        {
            base.FixedUpdatePass();
            _stephan.NavMeshAgent.destination = _player.transform.position;
        }

        public override StephanState SetNextState()
        {
            if (_distanceToPlayer <= 3f)
                return _stateMachine.IdleState;
        
            return this;
        }
    }
}