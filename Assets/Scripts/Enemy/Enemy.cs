using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

internal abstract class Enemy : MonoBehaviour
{
    [SerializeField] private protected Rigidbody _rigidbody;
    [SerializeField] private protected Animator _animator;
    [SerializeField] private protected LayerMask _groundLayer;
    [SerializeField] private protected LayerMask _playerLayer;
    [SerializeField] private protected NavMeshAgent _navMeshAgent;

    private protected Transform _target;

    public Animator Animator => _animator;
    public LayerMask PlayerLayer => _playerLayer;
    public LayerMask GroundLayer => _groundLayer;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;

    private protected abstract void InitializeStateMachine();
    
    private protected void Awake()
    {
        _target = FindObjectOfType<Player>().GetComponent<Transform>();
        InitializeStateMachine();
    }

    public virtual void ExecuteCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}