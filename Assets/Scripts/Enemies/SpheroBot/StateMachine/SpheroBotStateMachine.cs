using UnityEngine;

namespace Enemies.SpheroBot.StateMachine
{
    internal class SpheroBotStateMachine
    {
        private SpheroBotEntity _spheroBot;
        private SpheroBotData _data;
        private SpheroBotState _nextState;

        public SpheroBotState CurrentState { get; private set; }
        public SpheroBotIdleState IdleState { get; private set; }
        public SpheroBotPatrolState PatrolState { get; private set; }
        public SpheroBotActionState ActionState { get; private set; }

        public SpheroBotStateMachine(SpheroBotEntity spheroBot, SpheroBotData data)
        {
            _spheroBot = spheroBot;
            _data = data;
            InitializeStates();
            SetInitialState(PatrolState);
            _spheroBot.Provoked += OnProvoked;
            _spheroBot.TargetReceived += OnTargetReceived;
        }

        private void InitializeStates()
        {
            IdleState = new SpheroBotIdleState(this, _data, _spheroBot, "Idle");
            PatrolState = new SpheroBotPatrolState(this, _data, _spheroBot, "Move");
        }

        public void SetInitialState(SpheroBotState initialState)
        {
            CurrentState = initialState;
        }

        public void UpdatePass()
        {
            _nextState = CurrentState.SetNextState();

            if (_nextState == CurrentState)
                return;

            SwitchState(_nextState);
        }

        private void SwitchState(SpheroBotState nextState)
        {
            CurrentState.OnExit();
            CurrentState = nextState;
            CurrentState.OnEnter();
        }

        private void OnProvoked(Vector3 decoyPosition) =>
            SwitchState(new SpheroBotProvokedState(this, _spheroBot, _data, decoyPosition, "Move"));

        private void OnTargetReceived(Transform target) =>
            SwitchState(new SpheroBotActionState(this, _data, _spheroBot, "Roll", target));
    }
}