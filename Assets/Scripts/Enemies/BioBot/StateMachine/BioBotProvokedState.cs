using System.Collections;
using UnityEngine;

namespace Enemies.BioBot.StateMachine
{
    internal class BioBotProvokedState : BioBotState
    {
        private Vector3 _previousPosition;
        private Vector3 _decoyPosition;
        private bool _isProvoked;

        public BioBotProvokedState(BioBotStateMachine bioBotStateMachine, BioBotEntity bioBot, BioBotData data,
            Vector3 decoyPosition, string animationBoolName) : base(bioBotStateMachine, bioBot, data, animationBoolName)
        {
            _decoyPosition = decoyPosition;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _isProvoked = true;
            _previousPosition = _bioBot.transform.position;
            _bioBot.NavMeshAgent.enabled = true;
            _bioBot.ExecuteCoroutine(StartBeingProvoked());
        }

        public override void FixedUpdatePass()
        {
            base.FixedUpdatePass();
            MoveToDecoy();
        }

        public override void OnExit()
        {
            base.OnExit();
            _bioBot.NavMeshAgent.enabled = false;
        }

        public override BioBotState SetNextState()
        {
            if (!_isProvoked)
                return _stateMachine.PatrolState;

            return this;
        }

        //private void MoveToDecoy() => _bioBot.NavMeshAgent.destination = _decoyPosition;
        private void MoveToDecoy() => _bioBot.NavMeshAgent.SetDestination(_decoyPosition);

        private void ReturnToPreviousPosition() => _bioBot.NavMeshAgent.SetDestination(_previousPosition);

        //private void ReturnToPreviousPosition() => _bioBot.NavMeshAgent.destination = _previousPosition;


        private IEnumerator StartBeingProvoked()
        {
            MoveToDecoy();

            while (_bioBot.NavMeshAgent.hasPath)
            {
                yield return new WaitForFixedUpdate();
            }

            ReturnToPreviousPosition();

            while (_bioBot.NavMeshAgent.hasPath)
            {
                yield return new WaitForFixedUpdate();
            }

            _isProvoked = false;
        }
    }
}