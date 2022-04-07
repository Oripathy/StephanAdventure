using System;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

internal class Stephan : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private StephanData _data;
    [SerializeField] private FearBehaviour _fearBehaviour;
    [SerializeField] private Player _player;
    [SerializeField] private ResourceManager _resourceManager;
    [SerializeField] private GameObject _stephanModel;
    [SerializeField] private List<Transform> _hiddenSpots;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private float _stephanSize; //sizestep = 0.04
    private StephanStateMachine _stateMachine;

    public Animator Animator => _animator;
    public List<Transform> HiddenSpots => _hiddenSpots;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    public Rigidbody Rigidbody => _rigidbody;

    public event Action MaxSizeAchieved;
    public event Action SizeChanged;
    public event Action StepanHidden;
    public event Action StephanFound;

    private void Awake()
    {
        _stateMachine = new StephanStateMachine(this, _data, _player.transform);
        _resourceManager = _player.GetComponent<ResourceManager>();
        _fearBehaviour.StephanScared += () => StepanHidden?.Invoke();
        _resourceManager.FoodTaken += OnFoodTaken;
        _player.StephanFound += OnStephanFound;
    }

    private void Update()
    {
        _stateMachine.CurrentState.UpdatePass();
        _stateMachine.UpdatePass();
    }

    private void FixedUpdate()
    {
        _stateMachine.CurrentState.FixedUpdatePass();
    }

    private void OnFoodTaken(float value)
    {
        _stephanSize += value;
        var scale = _stephanModel.transform.localScale;
        scale += new Vector3(value, value, value);
        _stephanModel.transform.localScale = scale;
        SizeChanged?.Invoke();
        
        if (_stephanSize >= _data.MaxSize)
            MaxSizeAchieved?.Invoke();
    }

    private void OnStephanFound()
    {
        StephanFound?.Invoke();
    }
}