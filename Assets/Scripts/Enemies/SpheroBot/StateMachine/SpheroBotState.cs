namespace Enemies.SpheroBot.StateMachine
{
    internal abstract class SpheroBotState
    {
        private protected SpheroBotStateMachine _stateMachine;
        private protected SpheroBotEntity _spheroBot;
        private protected SpheroBotData _data;
        private protected string _animationBoolName;

        public SpheroBotState(SpheroBotStateMachine stateMachine, SpheroBotEntity spheroBot, SpheroBotData data,
            string animationBoolName)
        {
            _stateMachine = stateMachine;
            _data = data;
            _spheroBot = spheroBot;
            _animationBoolName = animationBoolName;
        }

        public virtual void OnEnter()
        {
            _spheroBot.Animator.SetBool(_animationBoolName, true);
        }

        public virtual void UpdatePass()
        {
        }

        public virtual void FixedUpdatePass()
        {
        }

        public virtual void OnExit()
        {
            _spheroBot.Animator.SetBool(_animationBoolName, false);
        }

        public abstract SpheroBotState SetNextState();
    }
}