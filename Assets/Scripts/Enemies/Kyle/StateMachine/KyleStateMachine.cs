using UnityEngine;

namespace Enemies.Kyle.StateMachine
{
    internal class KyleStateMachine
    {
        private KyleEntity _kyle;
        private KyleData _data;
        private KyleState _nextState;

        public KyleState CurrentState { get; private set; }
        public KylePatrolState PatrolState { get; private set; }
        public KyleIdleState IdleState { get; private set; }

        public KyleStateMachine(KyleEntity kyle, KyleData data)
        {
            _kyle = kyle;
            _data = data;
            InitializeStates();
            SetInitialState(PatrolState);
            _kyle.Provoked += OnProvoked;
            _kyle.Frozen += OnFrozen;
        }

        private void InitializeStates()
        {
            PatrolState = new KylePatrolState(this, _kyle, _data, "Move");
            IdleState = new KyleIdleState(this, _kyle, _data, "Idle");
        }

        public void UpdatePass()
        {
            _nextState = CurrentState.SetNextState();

            if (CurrentState == _nextState)
                return;

            SwitchState(_nextState);
        }

        private void SetInitialState(KyleState initialState)
        {
            CurrentState = initialState;
            CurrentState.OnEnter();
        }

        private void SwitchState(KyleState nextState)
        {
            CurrentState.OnExit();
            CurrentState = nextState;
            CurrentState.OnEnter();
        }

        private void OnProvoked(Vector3 decoyPosition) =>
            SwitchState(new KyleProvokedState(this, _kyle, _data, decoyPosition, "Move"));

        private void OnFrozen(float duration) =>
            SwitchState(new KyleFrozenState(this, _kyle, _data, duration, "Frozen"));
    }
}