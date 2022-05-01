namespace Enemies.BioBot.StateMachine
{
    internal abstract class BioBotState
    {
        private protected BioBotStateMachine _stateMachine;
        private protected BioBotEntity _bioBot;
        private protected BioBotData _data;
        private protected string _animationBoolName;

        public BioBotState(BioBotStateMachine bioBotStateMachine, BioBotEntity bioBot, BioBotData data,
            string animationBoolName)
        {
            _stateMachine = bioBotStateMachine;
            _bioBot = bioBot;
            _data = data;
            _animationBoolName = animationBoolName;
        }

        public virtual void OnEnter()
        {
            _bioBot.Animator.SetBool(_animationBoolName, true);
        }

        public virtual void OnExit()
        {
            _bioBot.Animator.SetBool(_animationBoolName, false);
        }

        public virtual void UpdatePass()
        {

        }

        public virtual void FixedUpdatePass()
        {

        }

        public abstract BioBotState SetNextState();
    }
}