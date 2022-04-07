using System.Runtime.CompilerServices;
using UnityEngine;

internal class StephanStateMachine
{
    private Stephan _stephan;
    private StephanData _data;
    private Transform _player;
    private StephanState _nextState;

    public StephanState CurrentState { get; private set; }

    public StephanFollowPlayerState FollowPlayerState { get; private set; }
    public StephanHideState HideState { get; private set; }
    public StephanIdleState IdleState { get; private set; }

    public StephanStateMachine(Stephan stephan, StephanData data, Transform player)
    {
        _stephan = stephan;
        _data = data;
        _player = player;
        
        InitializeStates();
        SetInitialState(IdleState);
        
        _stephan.StepanHidden += () =>
        {
            CurrentState.OnExit();
            CurrentState = HideState;
            CurrentState.OnEnter();
        };

        _stephan.StephanFound += () =>
        {
            CurrentState.OnExit();
            CurrentState = FollowPlayerState;
            CurrentState.OnEnter();
        };
    }

    private void InitializeStates()
    {
        FollowPlayerState = new StephanFollowPlayerState(this, _data, _stephan, _player, "Move");
        HideState = new StephanHideState(this, _data, _stephan, _player, "Idle");
        IdleState = new StephanIdleState(this, _data, _stephan, _player, "Idle");
    }

    private void SetInitialState(StephanState initialState)
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

    private void SwitchState(StephanState nextState)
    {
        CurrentState.OnExit();
        CurrentState = nextState;
        CurrentState.OnEnter();
    }
}