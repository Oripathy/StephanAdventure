using System;
using UnityEngine;
using Enemies.SpheroBot.StateMachine;
using Stephan;

namespace Enemies.SpheroBot
{
    internal class SpheroBotEntity : Enemy, IProvocable
    {
        [SerializeField] private SpheroBotData _data;

        private SpheroBotStateMachine _stateMachine;
        private float _actionTime;
        
        public Transform Target { get; private set; }

        public event Action<Vector3> Provoked;
        public event Action<Transform> TargetReceived;

        private void Update()
        {
            _stateMachine.CurrentState.UpdatePass();
            _stateMachine.UpdatePass();
        }

        private void FixedUpdate()
        {
            _stateMachine.CurrentState.FixedUpdatePass();
        }

        private protected override void OnAnimatorMove()
        {
            
        }

        private protected override void InitializeStateMachine() =>
            _stateMachine = new SpheroBotStateMachine(this, _data);
        
        private void OnTriggerEnter(Collider other)
        {
            if (Time.time < _actionTime)
                return;

            if (other.TryGetComponent<StephanEntity>(out var stephan))
            {
                Target = stephan.transform;
                _actionTime = Time.time + _data.ActionCooldown;
                TargetReceived?.Invoke(Target);
            }
        }

        public void Provoke(Vector3 decoyPosition) => Provoked?.Invoke(decoyPosition);
    }
}