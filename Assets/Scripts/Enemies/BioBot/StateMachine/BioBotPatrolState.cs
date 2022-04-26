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
            _bioBot.transform.position = MoveToPoint();
            _bioBot.transform.LookAt(new Vector3(_movePointsQueue.Peek().x, _bioBot.transform.position.y,
                _movePointsQueue.Peek().z));

            if (MoveToPoint() == _movePointsQueue.Peek())
            {
                _movePointsQueue.Dequeue();
                _movePointsQueue.Enqueue(_bioBot.transform.position);
                _isReachedPoint = true;
            }
        }

        private Vector3 MoveToPoint()
        {
            return Vector3.MoveTowards(_bioBot.transform.position, _movePointsQueue.Peek(),
                _data.MovementSpeed * Time.deltaTime);
        }

        public override BioBotState SetNextState()
        {
            if (_isReachedPoint)
                return _stateMachine.IdleState;

            return this;
        }
    }
}
