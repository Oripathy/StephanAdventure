using System;
using UnityEngine;
using Enemies.BioBot.StateMachine;

namespace Enemies.BioBot
{
    internal class BioBotEntity : Enemy, IProvocable, IFreezable
    {
        [SerializeField] private BioBotData _data;

        private BioBotStateMachine _stateMachine;

        public event Action<Vector3> Provoked;
        public event Action<float> Frozen;

        private void Update()
        {
            _stateMachine.CurrentState.UpdatePass();
            _stateMachine.UpdatePass();
        }

        private void FixedUpdate()
        {
            _stateMachine.CurrentState.FixedUpdatePass();
        }

        private protected override void InitializeStateMachine()
        {
            _stateMachine = new BioBotStateMachine(this, _data);
        }
        
        public void Provoke(Vector3 decoyPosition) => Provoked?.Invoke(decoyPosition);

        public void Freeze(float duration) => Frozen?.Invoke(duration);

    }
}
