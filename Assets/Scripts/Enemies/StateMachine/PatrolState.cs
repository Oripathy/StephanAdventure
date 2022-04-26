using System.Collections.Generic;
using UnityEngine;

namespace Enemies.StateMachine
{
    internal class PatrolState : State
    {
        private Queue<Vector3> _movePointsQueue;
        private bool _isReachedPoint;
    
        public PatrolState(global::Enemies.StateMachine.StateMachine stateMachine, Enemy enemy, EnemyData data, string animationBoolName) : base(
            stateMachine, enemy, data, animationBoolName)
        {
            _movePointsQueue = new Queue<Vector3>();
        
            foreach (var point in _enemy.MovePoints)
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
            _enemy.transform.position = MoveToPoint();

            if (MoveToPoint() == _movePointsQueue.Peek())
            {
                _movePointsQueue.Dequeue();
                _movePointsQueue.Enqueue(_enemy.transform.position);
                _isReachedPoint = true;
            }
        }

        public override State SetNextState()
        {
            if (_isReachedPoint)
                return _stateMachine.IdleState;
        
            return this;
        }

        private protected Vector3 MoveToPoint()
        {
            return Vector3.MoveTowards(_enemy.transform.position, _movePointsQueue.Peek(),
                _data.MovementSpeed * Time.deltaTime);
        }
    }
}