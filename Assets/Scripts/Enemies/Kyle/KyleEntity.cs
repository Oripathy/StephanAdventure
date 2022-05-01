using System;
using UnityEngine;
using Enemies.Kyle.StateMachine;

namespace Enemies.Kyle
{
    internal class KyleEntity : Enemy, IProvocable, IFreezable //MonoBehaviour
    {
        [SerializeField] private KyleData _data;

        private KyleStateMachine _stateMachine;

        public event Action<Vector3> Provoked;
        public event Action<float> Frozen;

        private protected override void InitializeStateMachine()
        {
            _stateMachine = new KyleStateMachine(this, _data);
        }

        private void Update()
        {
            _stateMachine.CurrentState.UpdatePass();
            _stateMachine.UpdatePass();
        }

        private void FixedUpdate()
        {
            _stateMachine.CurrentState.FixedUpdatePass();
        }

        public void Provoke(Vector3 decoyPosition) => Provoked?.Invoke(decoyPosition);

        public void Freeze(float duration) => Frozen?.Invoke(duration);
    }
}