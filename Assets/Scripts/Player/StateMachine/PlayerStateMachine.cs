namespace Player.StateMachine
{
    internal class PlayerStateMachine
    {
        private PlayerEntity _player;
        private PlayerData _data;
        private PlayerState _nextState;
        private PlayerInputHandler _inputHandler;
        private ResourceManager _resourceManager;
    
        public PlayerState CurrentState { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        //public PlayerThrowState ThrowState { get; private set; }

        public PlayerStateMachine(PlayerEntity player, PlayerData data, ResourceManager resourceManager)
        {
            _player = player;
            _data = data;
            _resourceManager = resourceManager;
            InitializeStates();
            _player.InputHandler.ThrowButtonPressed += OnThrowButtonPressed;
        }

        private void InitializeStates()
        {
            IdleState = new PlayerIdleState(this, _data, _player, "Idle");
            MoveState = new PlayerMoveState(this, _data, _player, "Move");
            //ThrowState = new PlayerThrowState(this, _data, _player, _resourceManager.Item, "Throw");
        }

        public void SetInitialState(PlayerState initialState)
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

        private void SwitchState(PlayerState nextState)
        {
            CurrentState.OnExit();
            CurrentState = nextState;
            CurrentState.OnEnter();
        }

        private void OnThrowButtonPressed()
        {
            if (CurrentState.GetType() != typeof(PlayerThrowState) && _resourceManager.Item != null)
                SwitchState(new PlayerThrowState(this, _data, _player, _resourceManager.Item, "Throw"));
        }
    }
}