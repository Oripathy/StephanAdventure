using System;
using UnityEngine;
using Enemies.SpheroBot.StateMachine;

namespace Enemies.SpheroBot
{
    internal class SpheroBotEntity : Enemy, IProvocable
    {
        //[SerializeField] private Transform _body;
        [SerializeField] private SpheroBotData _data;

        private SpheroBotStateMachine _stateMachine;

        //public Transform Body => _body;

        public event Action<Vector3> Provoked;

        private void Update()
        {
            _stateMachine.CurrentState.UpdatePass();
            _stateMachine.UpdatePass();
        }

        private void FixedUpdate()
        {
            _stateMachine.CurrentState.FixedUpdatePass();
        }

        private protected override void InitializeStateMachine() =>
            _stateMachine = new SpheroBotStateMachine(this, _data);

        public void Provoke(Vector3 decoyPosition) => Provoked?.Invoke(decoyPosition);
    }
}