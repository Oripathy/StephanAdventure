using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

internal class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PlayerInputHandler _inputHandler;
    [SerializeField] private PlayerData _data;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Transform _rightFoot;
    [SerializeField] private StageSwitcher _stageSwitcher;

    private PlayerStateMachine _stateMachine; 
    private Vector3 _velocity;
    private Vector3 _faceDirection;
    private bool _isStephanHidden;

    public Animator Animator => _animator;
    public PlayerInputHandler InputHandler => _inputHandler;
    public Transform RightFoot => _rightFoot;
    public LayerMask EnemyLayer => _enemyLayer;

    public event Action StephanFound;
    
    private void Awake()
    {
        _stageSwitcher.StephanHidden += OnStephanHidden;
        InitializeStateMachine();
        _stateMachine.SetInitialState(_stateMachine.IdleState);
        _faceDirection = new Vector3(0f, 0f, 1f);
    }

    private void Update()
    {
        _stateMachine.CurrentState.UpdatePass();
        _stateMachine.UpdatePass();
    }

    private void FixedUpdate()
    {
        _stateMachine.CurrentState.FixedUpdatePass();
        _rigidbody.MovePosition(_rigidbody.position + _velocity * Time.deltaTime);
    }

    private void InitializeStateMachine()
    {
        _stateMachine = new PlayerStateMachine(this, _data);
    }

    public void SetVelocity(Vector3 velocity)
    {
        _velocity = velocity;
    }

    public Vector3 CalculateMoveDirectionOnSurface()
    {
        if (Physics.Raycast(transform.position, _inputHandler.MoveDirection, out var hit, 0.3f,
                _groundLayer))
        {
            if (Vector3.Angle(Vector3.up, hit.normal) <= 60f)
            {
                var newDirection = _inputHandler.MoveDirection -
                                   Vector3.Dot(_inputHandler.MoveDirection, hit.normal) * hit.normal;
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
                return newDirection;
            }
        }
        
        return _inputHandler.MoveDirection;
    }

    public void Rotate()
    {
        _faceDirection = _inputHandler.MoveDirection;
        _rigidbody.transform.LookAt(_faceDirection + _rigidbody.transform.position);
    }

    public void ExecuteCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isStephanHidden)
            return;

        if (other.TryGetComponent<Stephan>(out var stephan))
        {
            StephanFound?.Invoke();
            _isStephanHidden = false;
        }
    }

    private void OnStephanHidden() => _isStephanHidden = true;
}