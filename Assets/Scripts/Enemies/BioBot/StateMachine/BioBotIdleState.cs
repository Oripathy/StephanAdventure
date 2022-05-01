using System.Collections;
using UnityEngine;

namespace Enemies.BioBot.StateMachine
{
    internal class BioBotIdleState : BioBotState
    {
        private bool _isWaitTimeEnded;

        public BioBotIdleState(BioBotStateMachine stateMachine, BioBotEntity bioBot, BioBotData data,
            string animationBoolName) :
            base(stateMachine, bioBot, data, animationBoolName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _isWaitTimeEnded = false;
            _bioBot.ExecuteCoroutine(Wait());
        }

        public override BioBotState SetNextState()
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