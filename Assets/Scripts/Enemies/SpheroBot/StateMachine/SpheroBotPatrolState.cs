using System.Collections.Generic;
using UnityEngine;

namespace Enemies.SpheroBot.StateMachine
{
    internal class SpheroBotPatrolState : SpheroBotState
    {
        private Queue<Vector3> _movePointsQueue;
        private bool _isReachedPoint;

        public SpheroBotPatrolState(SpheroBotStateMachine stateMachine, SpheroBotData data, SpheroBotEntity spheroBot,
            string animationBoolName) : base(stateMachine, spheroBot, data, animationBoolName)
        {
            _movePointsQueue = new Queue<Vector3>();

            foreach (var point in _spheroBot.MovePoints)
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
            _spheroBot.transform.position = MoveToPoint();
            _spheroBot.transform.LookAt(new Vector3(_movePointsQueue.Peek().x, _spheroBot.transform.position.y,
                _movePointsQueue.Peek().z));

            if (MoveToPoint() == _movePointsQueue.Peek())
            {
                _movePointsQueue.Dequeue();
                _movePointsQueue.Enqueue(_spheroBot.transform.position);
                _isReachedPoint = true;
            }
        }

        public override SpheroBotState SetNextState()
        {
            if (_isReachedPoint)
                return _stateMachine.IdleState;

            return this;
        }

        private Vector3 MoveToPoint()
        {
            return Vector3.MoveTowards(_spheroBot.transform.position, _movePointsQueue.Peek(),
                _data.MovementSpeed * Time.deltaTime);
        }
    }
}