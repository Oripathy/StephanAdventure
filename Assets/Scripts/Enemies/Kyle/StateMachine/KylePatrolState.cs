using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Kyle.StateMachine
{
    internal class KylePatrolState : KyleState
    {
        private Queue<Vector3> _movePointsQueue;
        private bool _isReachedPoint;

        public KylePatrolState(KyleStateMachine stateMachine, KyleEntity kyle, KyleData data,
            string animationBoolName) : base(
            stateMachine, kyle, data, animationBoolName)
        {
            _movePointsQueue = new Queue<Vector3>();

            foreach (var point in _kyle.MovePoints)
                _movePointsQueue.Enqueue(point.position);
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _isReachedPoint = false;
        }

        public override void FixedUpdatePass()
        {
            base.FixedUpdatePass();
            RotateAt(_movePointsQueue.Peek());

             if (Vector3.Distance(_kyle.transform.position, _movePointsQueue.Peek()) <= 0.2f)
            {
                _movePointsQueue.Dequeue();
                _movePointsQueue.Enqueue(_kyle.transform.position);
                _isReachedPoint = true;
            }
        }

        public override KyleState SetNextState()
        {
            if (_isReachedPoint)
                return _stateMachine.IdleState;

            return this;
        }

        private void RotateAt(Vector3 point)
        {
            Vector3 relativePosition = new Vector3(point.x - _kyle.transform.position.x, 0f,
                point.z - _kyle.transform.position.z);
            Quaternion rotation = Quaternion.LookRotation(relativePosition, Vector3.up);
            _kyle.transform.rotation = Quaternion.RotateTowards(_kyle.transform.rotation, rotation,
                _data.RotationSpeed * Time.deltaTime);
        }
    }
}