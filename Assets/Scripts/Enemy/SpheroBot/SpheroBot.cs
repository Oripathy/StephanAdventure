using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

internal class SpheroBot : Enemy
{
    [SerializeField] private Transform _body;
    [SerializeField] private SpheroBotData _data;

    private Vector3 _velocity;
    private SpheroBotStateMachine _stateMachine;
    
    public Transform Body => _body;

    // private void Awake()
    // {
    //     InitializeStateMachine();
    //     _target = FindObjectOfType<Player>().GetComponent<Transform>();
    // }

    // private void Start()
    // {
    //     _navMeshAgent.enabled = true;
    // }

    private void Update()
    {
        _stateMachine.CurrentState.UpdatePass();
        _stateMachine.UpdatePass();
    }

    private void FixedUpdate()
    {
        _stateMachine.CurrentState.FixedUpdatePass();
    }

    private protected override void InitializeStateMachine()
    {
        _stateMachine = new SpheroBotStateMachine(this, _data, _target);
        _stateMachine.SetInitialState(_stateMachine.IdleState);
    }

    public void SetVelocity(Vector3 velocity)
    {
        _velocity = velocity;
    }
}