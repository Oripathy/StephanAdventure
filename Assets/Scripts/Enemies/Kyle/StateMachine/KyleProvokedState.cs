using System.Collections;
using UnityEngine;

namespace Enemies.Kyle.StateMachine
{
    internal class KyleProvokedState : KyleState
    {
        private Vector3 _decoyPosition;
        private Vector3 _previousPosition;
        private bool _isProvoked;

        public KyleProvokedState(KyleStateMachine stateMachine, KyleEntity kyle, KyleData data, Vector3 decoyPosition,
            string animationBoolName) : base(stateMachine, kyle, data, animationBoolName)
        {
            _decoyPosition = decoyPosition;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _previousPosition = _kyle.transform.position;
            _kyle.Rigidbody.isKinematic = true;
            _isProvoked = true;
            _kyle.NavMeshAgent.enabled = true;
            _kyle.NavMeshAgent.updatePosition = false;
            _kyle.NavMeshAgent.updateRotation = false;
            _kyle.Animator.speed = 1.5f;
            _kyle.ExecuteCoroutine(StartBeingProvoked());
        }

        public override void UpdatePass()
        {
            base.UpdatePass();
            MoveNavMeshAgent();
        }

        public override void OnExit()
        {
            base.OnExit();
            _kyle.NavMeshAgent.enabled = false;
            _kyle.Rigidbody.isKinematic = false;
            _kyle.Animator.speed = 1f;
        }

        public override KyleState SetNextState()
        {
            if (!_isProvoked)
                return _stateMachine.PatrolState;

            return this;
        }

        private void MoveToDecoy() => _kyle.NavMeshAgent.SetDestination(_decoyPosition);
        private void ReturnToPreviousPosition() => _kyle.NavMeshAgent.SetDestination(_previousPosition);

        private IEnumerator StartBeingProvoked()
        {
            MoveToDecoy();

            while (Vector3.Distance(_kyle.transform.position, _decoyPosition) >= 0.5f)
            {
                if (!IsCollidedWithObstacle())
                    yield return null;
                else
                    break;
            }

            ReturnToPreviousPosition();

            while (Vector3.Distance(_kyle.transform.position, _previousPosition) >= 0.5f)
                yield return null;

            _isProvoked = false;
        }

        private void MoveNavMeshAgent()
        {
            Vector3 worldDeltaPosition = _kyle.NavMeshAgent.nextPosition - _kyle.transform.position;
            RotateAt(_kyle.NavMeshAgent.nextPosition);

            if (worldDeltaPosition.magnitude > _kyle.NavMeshAgent.radius)
                _kyle.transform.position = _kyle.NavMeshAgent.nextPosition - 0.9f * worldDeltaPosition;
        }
        
        private void RotateAt(Vector3 point)
        {
            Vector3 relativePosition = new Vector3(point.x - _kyle.transform.position.x, 0f,
                point.z - _kyle.transform.position.z);
            Quaternion rotation = Quaternion.LookRotation(relativePosition, Vector3.up);
            _kyle.transform.rotation = Quaternion.RotateTowards(_kyle.transform.rotation, rotation,
                _data.RotationSpeed * Time.deltaTime);
        }

        private bool IsCollidedWithObstacle()
        {
            var rayPosition = new Vector3(_kyle.transform.position.x, 1f, _kyle.transform.position.z);
            return Physics.Raycast(rayPosition, _kyle.transform.forward, 0.4f);
        }
    }
}