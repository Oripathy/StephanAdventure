using System.Collections;
using UnityEngine;

namespace Enemies.SpheroBot.StateMachine
{
    internal class SpheroBotProvokedState : SpheroBotState
    {
        private Vector3 _decoyPosition;
        private Vector3 _previousPosition;
        private bool _isProvoked;

        public SpheroBotProvokedState(SpheroBotStateMachine stateMachine, SpheroBotEntity spheroBot, SpheroBotData data,
            Vector3 decoyPosition,
            string animationBoolName) : base(stateMachine, spheroBot, data, animationBoolName)
        {
            _decoyPosition = decoyPosition;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _previousPosition = _spheroBot.transform.position;
            _isProvoked = true;
            _spheroBot.NavMeshAgent.enabled = true;
            _spheroBot.ExecuteCoroutine(StartBeingProvoked());
        }

        public override void OnExit()
        {
            base.OnExit();
            _spheroBot.NavMeshAgent.enabled = false;
        }

        public override SpheroBotState SetNextState()
        {
            if (!_isProvoked)
                return _stateMachine.PatrolState;

            return this;
        }

        private void MoveToDecoy() => _spheroBot.NavMeshAgent.SetDestination(_decoyPosition);
        private void ReturnToPreviousPosition() => _spheroBot.NavMeshAgent.SetDestination(_previousPosition);

        private IEnumerator StartBeingProvoked()
        {
            MoveToDecoy();

            while (_spheroBot.NavMeshAgent.hasPath)
                yield return null;

            ReturnToPreviousPosition();

            while (_spheroBot.NavMeshAgent.hasPath)
                yield return null;

            _isProvoked = false;
        }
    }
}