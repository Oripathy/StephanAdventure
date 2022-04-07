using UnityEngine;

internal class PlayerStateMachine
{
    private Player _player;
    private PlayerData _data;
    private PlayerState _nextState;
    private PlayerInputHandler _inputHandler;
    private bool _attackButtonPressed;
    
    public PlayerState CurrentState { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerKickState KickState { get; private set; }
    public bool AttackButtonPressed => _attackButtonPressed;

    public PlayerStateMachine(Player player, PlayerData data)
    {
        _player = player;
        _data = data;
        InitializeStates();
        _player.InputHandler.AttackButtonPressed += OnAttackButtonPressed;
    }

    private void InitializeStates()
    {
        IdleState = new PlayerIdleState(this, _data, _player, "Idle");
        MoveState = new PlayerMoveState(this, _data, _player, "Move");
        KickState = new PlayerKickState(this, _data, _player, "Kick");
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
        _attackButtonPressed = false;
        CurrentState.OnExit();
        CurrentState = nextState;
        CurrentState.OnEnter();
    }

    private void OnAttackButtonPressed() => _attackButtonPressed = true;
}