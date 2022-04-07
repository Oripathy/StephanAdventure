using UnityEngine;

internal class StephanHideState : StephanState
{
    public StephanHideState(StephanStateMachine stateMachine, StephanData data, Stephan stephan, Transform player,
        string animationBoolName) : base(stateMachine, data, stephan, player, animationBoolName)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        //_stephan.Rigidbody.velocity = new Vector3(0f, 0f, 0f);
        _stephan.NavMeshAgent.enabled = false;
        TeleportToHiddenSpot();
    }

    public override void OnExit()
    {
        base.OnExit();
        _stephan.NavMeshAgent.enabled = true;
    }

    public override StephanState SetNextState()
    {
        return this;
    }

    private void TeleportToHiddenSpot()
    {
        var point = SelectHiddenSpot().position;
        _stephan.transform.position = point;//SelectHiddenSpot().position;
        Debug.Log($"stepan's posision : {_stephan.transform.position}, point : {point}");
    }

    private Transform SelectHiddenSpot()
    {
        var index = (int) Random.Range(0f, _stephan.HiddenSpots.Count);
        return _stephan.HiddenSpots[index];
    }
}