using System.Collections;
using UnityEngine;

namespace Enemies.StateMachine
{
    internal class IdleState : State
    {
        private protected bool _isWaitTimeEnded;

        public IdleState(global::Enemies.StateMachine.StateMachine stateMachine, Enemy enemy, EnemyData data, string animationBoolName) : base(
            stateMachine, enemy, data, animationBoolName)
        {

        }

        public override void OnEnter()
        {
            base.OnEnter();
            _isWaitTimeEnded = false;
        
        }

        public override State SetNextState()
        {
            if (_isWaitTimeEnded)
                return this;

            return this;
        }

        private protected IEnumerator Wait()
        {
            yield return new WaitForSeconds(_data.WaitTime);
            _isWaitTimeEnded = true;
        }
    }
}