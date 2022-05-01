using System;
using System.Collections.Generic;
using Player;
using Stephan.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Stephan
{
    internal class StephanEntity : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private StephanData _data;
        [SerializeField] private FearBehaviour _fearBehaviour;
        [SerializeField] private PlayerEntity _player;
        [SerializeField] private ResourceManager _resourceManager;
        [SerializeField] private GameObject _stephanModel;
        [SerializeField] private List<Transform> _hiddenSpots;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private float _stephanAddedSize; //sizestep = 0.04
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
             // _stephanAddedSize = 0.36f;
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

        private void OnFoodTaken()
        {
            _stephanAddedSize += _data.SizeStep;
            _stephanModel.transform.localScale += new Vector3(1f, 1f, 1f) * _data.SizeStep;
            SizeChanged?.Invoke();
        
            if (_stephanAddedSize >= _data.MaxAddedSize - 0.01f)
                MaxSizeAchieved?.Invoke();
        }

        private void OnStephanFound()
        {
            StephanFound?.Invoke();
        }
    }
}