namespace Enemies.Kyle.StateMachine
{
    internal abstract class KyleState
    {
        private protected KyleStateMachine _stateMachine;
        private protected KyleEntity _kyle;
        private protected KyleData _data;
        private string _animationBoolName;

        public KyleState(KyleStateMachine stateMachine, KyleEntity kyle, KyleData data, string animationBoolName)
        {
            _stateMachine = stateMachine;
            _kyle = kyle;
            _data = data;
            _animationBoolName = animationBoolName;
        }

        public virtual void OnEnter()
        {
            _kyle.Animator.SetBool(_animationBoolName, true);
        }

        public virtual void UpdatePass()
        {

        }

        public virtual void FixedUpdatePass()
        {

        }

        public virtual void OnExit()
        {
            _kyle.Animator.SetBool(_animationBoolName, false);
        }

        public abstract KyleState SetNextState();
    }
}
