

using UnityEngine;

internal abstract class SpheroBotState
{
    private protected SpheroBotStateMachine _stateMachine;
    private protected SpheroBotData _data;
    private protected Transform _target;
    private protected SpheroBot _spheroBot;
    private protected string _animationBoolName;
    private protected Vector3 _moveDirection;
    private protected float _distanceToTarget;

    public SpheroBotState(SpheroBotStateMachine stateMachine, SpheroBotData data, SpheroBot spheroBot, Transform target,
        string animationBoolName)
    {
        _stateMachine = stateMachine;
        _data = data;
        _spheroBot = spheroBot;
        _target = target;
        _animationBoolName = animationBoolName;
    }

    public virtual void OnEnter()
    {
        _spheroBot.Animator.SetBool(_animationBoolName, true);
        Debug.Log(this);
    }

    public virtual void UpdatePass()
    {
        CalculateDistanceToTarget();
    }

    public virtual void FixedUpdatePass()
    {
        
    }

    public virtual void OnExit()
    {
        _spheroBot.Animator.SetBool(_animationBoolName, false);
    }

    private protected void CalculateDistanceToTarget()
    {
        _distanceToTarget = Vector3.Distance(_target.position, _spheroBot.transform.position);
    }

    public abstract SpheroBotState SetNextState();
}