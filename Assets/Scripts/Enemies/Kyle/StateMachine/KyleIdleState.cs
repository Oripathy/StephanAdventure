using System.Collections;
using UnityEngine;

namespace Enemies.Kyle.StateMachine
{
    internal class KyleIdleState : KyleState
    {
        private bool _isWaitTimeEnded;

        public KyleIdleState(KyleStateMachine stateMachine, KyleEntity kyle, KyleData data,
            string animationBoolName) : base(
            stateMachine, kyle, data, animationBoolName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _isWaitTimeEnded = false;
            _kyle.ExecuteCoroutine(Wait());
        }

        public override KyleState SetNextState()
        {
            if (_isWaitTimeEnded)
                return _stateMachine.PatrolState;

            return this;
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(_data.WaitTime);
            _isWaitTimeEnded = true;
        }
    }
}