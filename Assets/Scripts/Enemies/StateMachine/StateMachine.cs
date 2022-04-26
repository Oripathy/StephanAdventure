using UnityEngine;

namespace Enemies.StateMachine
{
    internal abstract class StateMachine
    {
        private Enemy _enemy;
        private EnemyData _data;
        private State _nextState;

        public State CurrentState { get; private set; }
        public IdleState IdleState { get; private set; }
        public PatrolState PatrolState { get; private set; }
    
        public StateMachine(Enemy enemy, EnemyData data)
        {
            _enemy = enemy;
            _data = data;
        }

        private protected abstract void InitializeStates();

        private protected virtual void SetInitialState(State initialState)
        {
            CurrentState = initialState;
            CurrentState.OnEnter();
        }

        public virtual void UpdatePass()
        {
        
        }

        private protected void SwitchState(State nextState)
        {
            CurrentState.OnExit();
            CurrentState = nextState;
            CurrentState.OnEnter();
        }

        private protected void OnProvoked(Vector3 decoyPosition) =>
            SwitchState(new ProvokedState(this, _enemy, _data, decoyPosition, "Move"));

        private protected void OnFrozen(float duration) =>
            SwitchState(new FrozenState(this, _enemy, _data, duration, "Frozen"));
    }
}