using UnityEngine;

namespace Enemies.BioBot.StateMachine
{
    internal class BioBotStateMachine
    {
        private BioBotEntity _bioBot;
        private BioBotData _data;
        private BioBotState _nextState;

        public BioBotState CurrentState { get; private set; }
        public BioBotPatrolState PatrolState { get; private set; }
        public BioBotIdleState IdleState { get; private set; }
        public BioBotActionState ActionState { get; private set; }

        public BioBotStateMachine(BioBotEntity bioBot, BioBotData data)
        {
            _bioBot = bioBot;
            _data = data;
            InitializeStates();
            SetInitialState(PatrolState);
            _bioBot.Provoked += OnProvoked;
            _bioBot.Frozen += OnFrozen;
        }

        private void InitializeStates()
        {
            PatrolState = new BioBotPatrolState(this, _bioBot, _data, "Move");
            IdleState = new BioBotIdleState(this, _bioBot, _data, "Idle");
            ActionState = new BioBotActionState(this, _bioBot, _data, "Action");
        }

        private void SetInitialState(BioBotState initialState)
        {
            CurrentState = initialState;
            CurrentState.OnEnter();
        }

        public void UpdatePass()
        {
            _nextState = CurrentState.SetNextState();

            if (_nextState == CurrentState)
                return;

            SwitchState(_nextState);
        }

        private void SwitchState(BioBotState nextState)
        {
            CurrentState.OnExit();
            CurrentState = nextState;
            CurrentState.OnEnter();
        }

        private void OnProvoked(Vector3 decoyPosition)
        {
            if (CurrentState.GetType() != typeof(BioBotFrozenState))
                SwitchState(new BioBotProvokedState(this, _bioBot, _data, decoyPosition, "Move"));
        }

        private void OnFrozen(float duration)
        {
            SwitchState(new BioBotFrozenState(this, _bioBot, _data, duration, "Frozen"));
        }
    }
}
