using System.Collections;
using UnityEngine;

namespace Enemies.SpheroBot.StateMachine
{
    internal class SpheroBotIdleState : SpheroBotState
    {
        private bool _isWaitTimeEnded;

        public SpheroBotIdleState(SpheroBotStateMachine stateMachine, SpheroBotData data, SpheroBotEntity spheroBot,
            string animationBoolName) : base(stateMachine, spheroBot, data, animationBoolName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _isWaitTimeEnded = false;
            _spheroBot.ExecuteCoroutine(Wait());
        }

        public override SpheroBotState SetNextState()
        {
            if (_isWaitTimeEnded)
                return _stateMachine.PatrolState;
            
            return this;
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(2f);
            _isWaitTimeEnded = true;
        }
    }
}