using UnityEngine;

internal class SpheroBotStateMachine
{
    private SpheroBot _spheroBot;
    private SpheroBotData _data;
    private SpheroBotState _nextState;
    private Transform _target;

    public SpheroBotState CurrentState { get; private set; }
    public SpheroBotIdleState IdleState { get; private set; }
    public SpheroBotMoveState MoveState { get; private set; }
    public SpheroBotRollState RollState { get; private set; }
    public SpheroBotAttackState AttackState { get; private set; }
    public SpheroBotBounceAttack BounceAttack { get; private set; }
    public SpheroBotSearchForTargetState SearchForTargetState { get; private set; }

    public SpheroBotStateMachine(SpheroBot spheroBot, SpheroBotData data, Transform target)
    {
        _spheroBot = spheroBot;
        _data = data;
        _target = target;
        InitializeStates();
    }

    private void InitializeStates()
    {
        IdleState = new SpheroBotIdleState(this, _data, _spheroBot, _target, "Idle");
        MoveState = new SpheroBotMoveState(this, _data, _spheroBot, _target, "Move");
        RollState = new SpheroBotRollState(this, _data, _spheroBot, _target, "Roll");
        AttackState = new SpheroBotAttackState(this, _data, _spheroBot, _target, "Attack");
        BounceAttack = new SpheroBotBounceAttack(this, _data, _spheroBot, _target, "BounceAttack");
        SearchForTargetState = new SpheroBotSearchForTargetState(this, _data, _spheroBot, _target, "SearchForTarget");
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
}