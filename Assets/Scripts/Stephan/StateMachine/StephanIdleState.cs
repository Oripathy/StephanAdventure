using UnityEngine;

namespace Stephan.StateMachine
{
    internal class StephanIdleState : StephanState
    {
        public StephanIdleState(StephanStateMachine stateMachine, StephanData data, StephanEntity stephan, Transform player,
            string animationBoolName) : base(stateMachine, data, stephan, player, animationBoolName)
        {
        }

        public override StephanState SetNextState()
        {
            if (_distanceToPlayer > 2f)
                return _stateMachine.FollowPlayerState;
        
            return this;
        }
    }
}