using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    internal abstract class Enemy : MonoBehaviour, IMovePointsSetter
    {
        [SerializeField] private protected Rigidbody _rigidbody;
        [SerializeField] private protected Animator _animator;
        [SerializeField] private protected LayerMask _groundLayer;
        [SerializeField] private protected LayerMask _stephanLayer;
        [SerializeField] private protected LayerMask _playerLayer;
        [SerializeField] private protected NavMeshAgent _navMeshAgent;
        [SerializeField] private protected List<Transform> _movePoints;

        //private protected Transform _target;
        private protected Coroutine _coroutine;

        public Rigidbody Rigidbody => _rigidbody;
        public Animator Animator => _animator;
        public LayerMask PlayerLayer => _playerLayer;
        public LayerMask GroundLayer => _groundLayer;
        public LayerMask StephanLayer => _stephanLayer;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        public List<Transform> MovePoints => _movePoints;

        private protected abstract void InitializeStateMachine();

        private protected void Start()
        {
            InitializeStateMachine();
            _navMeshAgent.enabled = false;
        }

        private protected virtual void OnAnimatorMove()
        {
            Vector3 position = _animator.rootPosition;
            position.y = _navMeshAgent.nextPosition.y;
            transform.position = position;
        }

        public void ExecuteCoroutine(IEnumerator coroutine)
        {
            _coroutine = StartCoroutine(coroutine);
        }

        public void SetMovePoints(List<Transform> movePoints)
        {
            _movePoints = movePoints;
        }
    }
}