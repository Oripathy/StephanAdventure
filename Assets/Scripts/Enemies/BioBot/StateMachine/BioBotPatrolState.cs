using System.Collections.Generic;
using UnityEngine;

namespace Enemies.BioBot.StateMachine
{
    internal class BioBotPatrolState : BioBotState
    {
        private Queue<Vector3> _movePointsQueue;
        private bool _isReachedPoint;

        public BioBotPatrolState(BioBotStateMachine bioBotStateMachine, BioBotEntity bioBot, BioBotData data,
            string animationBoolName) : base(bioBotStateMachine, bioBot, data, animationBoolName)
        {
            _movePointsQueue = new Queue<Vector3>();

            foreach (var point in _bioBot.MovePoints)
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

            if (Vector3.Distance(_bioBot.transform.position, _movePointsQueue.Peek()) <= 0.1f)
            {
                _movePointsQueue.Dequeue();
                _movePointsQueue.Enqueue(_bioBot.transform.position);
                _isReachedPoint = true;
            }
        }

        private void RotateAt(Vector3 point)
        {
            Vector3 relativePosition = new Vector3(point.x - _bioBot.transform.position.x, 0f,
                point.z - _bioBot.transform.position.z);
            Quaternion rotation = Quaternion.LookRotation(relativePosition, Vector3.up);
            _bioBot.transform.rotation = Quaternion.RotateTowards(_bioBot.transform.rotation, rotation,
                _data.RotationSpeed * Time.deltaTime);
        }

        public override BioBotState SetNextState()
        {
            if (_isReachedPoint)
                return _stateMachine.IdleState;

            return this;
        }
    }
}
