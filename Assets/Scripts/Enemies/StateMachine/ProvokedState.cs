using System.Collections;
using UnityEngine;

namespace Enemies.StateMachine
{
    internal class ProvokedState : State
    {
        private Vector3 _decoyPosition;
        private Vector3 _previousPosition;
        private bool _isProvoked;

        public ProvokedState(global::Enemies.StateMachine.StateMachine stateMachine, Enemy enemy, EnemyData data, Vector3 decoyPosition,
            string animationBoolName) : base(stateMachine, enemy, data, animationBoolName)
        {
            _decoyPosition = decoyPosition;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _previousPosition = _enemy.transform.position;
            _isProvoked = true;
            _enemy.NavMeshAgent.enabled = true;
        }

        public override void OnExit()
        {
            base.OnExit();
            _enemy.NavMeshAgent.enabled = false;
        }

        public override State SetNextState()
        {
            if (!_isProvoked)
                return _stateMachine.PatrolState;

            return this;
        }

        private void MoveToDecoy() => _enemy.NavMeshAgent.SetDestination(_decoyPosition);
        private void ReturnToPreviousPosition() => _enemy.NavMeshAgent.SetDestination(_previousPosition);

        private IEnumerator StartBeingProvoked()
        {
            MoveToDecoy();

            while (_enemy.NavMeshAgent.hasPath)
                yield return null;

            ReturnToPreviousPosition();

            while (_enemy.NavMeshAgent.hasPath)
                yield return null;

            _isProvoked = false;
        }
    }
}
