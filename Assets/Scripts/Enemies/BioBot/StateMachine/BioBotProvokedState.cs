using System;
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
            _bioBot.Rigidbody.isKinematic = true;
            _bioBot.NavMeshAgent.enabled = true;
            _bioBot.NavMeshAgent.updatePosition = false;
            _bioBot.NavMeshAgent.updateRotation = false;
            _bioBot.Animator.speed = 1.5f;
            _bioBot.ExecuteCoroutine(StartBeingProvoked());
        }

        public override void UpdatePass()
        {
            base.UpdatePass();
            MoveMavMeshAgent();
        }

        public override void OnExit()
        {
            base.OnExit();
            _bioBot.NavMeshAgent.enabled = false;
            _bioBot.Rigidbody.isKinematic = false;
            _bioBot.Animator.speed = 1f;
        }

        public override BioBotState SetNextState()
        {
            if (!_isProvoked)
                return _stateMachine.PatrolState;

            return this;
        }

        private void MoveMavMeshAgent()
        {
            Vector3 worldDeltaPosition = _bioBot.NavMeshAgent.nextPosition - _bioBot.transform.position;
            RotateAt(_bioBot.NavMeshAgent.nextPosition);

            if (worldDeltaPosition.magnitude > _bioBot.NavMeshAgent.radius)
                _bioBot.transform.position = _bioBot.NavMeshAgent.nextPosition - 0.9f * worldDeltaPosition;
        }

        private void RotateAt(Vector3 point)
        {
            Vector3 relativePosition = new Vector3(point.x - _bioBot.transform.position.x, 0f,
                point.z - _bioBot.transform.position.z);
            Quaternion rotation = Quaternion.LookRotation(relativePosition, Vector3.up);
            _bioBot.transform.rotation = Quaternion.RotateTowards(_bioBot.transform.rotation, rotation,
                _data.RotationSpeed * Time.deltaTime);
        }
        
        private void MoveToDecoy() => _bioBot.NavMeshAgent.SetDestination(_decoyPosition);

        private void ReturnToPreviousPosition() => _bioBot.NavMeshAgent.SetDestination(_previousPosition);

        private IEnumerator StartBeingProvoked()
        {
            MoveToDecoy();

            while (Vector3.Distance(_bioBot.transform.position, _decoyPosition) >= 0.5f)
            {
                if (!IsCollideWithObstacle())
                    yield return new WaitForFixedUpdate();
                else
                    break;
            }

            ReturnToPreviousPosition();

            while (Vector3.Distance(_bioBot.transform.position, _previousPosition) >= 0.5f)
            {
                yield return new WaitForFixedUpdate();
            }

            _isProvoked = false;
        }

        private bool IsCollideWithObstacle()
        {
            var rayPosition = new Vector3(_bioBot.transform.position.x, 1f, _bioBot.transform.position.z);
            return Physics.Raycast(rayPosition, _bioBot.transform.forward, 0.4f);
        }
    }
}