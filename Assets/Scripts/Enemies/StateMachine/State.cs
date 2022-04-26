namespace Enemies.StateMachine
{
    internal abstract class State
    {
        private protected global::Enemies.StateMachine.StateMachine _stateMachine;
        private protected Enemy _enemy;
        private protected EnemyData _data;
        private protected string _animationBoolName;

        public State(global::Enemies.StateMachine.StateMachine stateMachine, Enemy enemy, EnemyData data, string animationBoolName)
        {
            _stateMachine = stateMachine;
            _enemy = enemy;
            _data = data;
            _animationBoolName = animationBoolName;
        }
    
        public virtual void OnEnter()
        {
            _enemy.Animator.SetBool(_animationBoolName, true);
        }

        public virtual void UpdatePass()
        {
        
        }

        public virtual void FixedUpdatePass()
        {
        
        }

        public virtual void OnExit()
        {
            _enemy.Animator.SetBool(_animationBoolName, false);
        }

        public abstract State SetNextState();
    }
}