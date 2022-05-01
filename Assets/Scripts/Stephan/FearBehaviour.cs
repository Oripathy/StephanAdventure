using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;

namespace Stephan
{
    internal class FearBehaviour : MonoBehaviour, IFearApplier
    {
        [SerializeField] private StephanData _data;
        [SerializeField] private StephanEntity _stephan;

        private Dictionary<Collider, Coroutine> _scareSphereDictionary;
        private bool _isGettingScared;
        private float _fearAmount;
        private float _fearAmountDecrease;

        public event Action<float> FearAmountChanged;
        public event Action StephanScared;

        private void Awake()
        {
            _fearAmountDecrease = _data.FearAmountDecrease;
            _scareSphereDictionary = new Dictionary<Collider, Coroutine>();
            _stephan.StephanFound += ResetFearAmount;
        }

        private void FixedUpdate()
        {
            if (!_isGettingScared && _fearAmount > 0)
                DecreaseFearAmount(_fearAmountDecrease);
        }

        private void DecreaseFearAmount(float value)
        {
            _fearAmount -= value;
            FearAmountChanged?.Invoke(_fearAmount);

            if (_fearAmount < 0)
                _fearAmount = 0f;
        }
    
        private IEnumerator IncreaseFearAmount(float value)
        {
            while (_fearAmount < _data.FearAmountLimit)
            {
                _fearAmount += value;
                FearAmountChanged?.Invoke(_fearAmount);
                yield return new WaitForFixedUpdate();
            }

            if (_fearAmount > _data.FearAmountLimit)
                _fearAmount = _data.FearAmountLimit;
        
            StephanScared?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ScareSphere>(out var scareSphere))
            {
                _isGettingScared = true;
                var coroutine = StartCoroutine(IncreaseFearAmount(scareSphere.FearAmountToReceive));
                _scareSphereDictionary.Add(other, coroutine);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<ScareSphere>(out var scareSphere))
            {
                _scareSphereDictionary.TryGetValue(other, out var coroutine);
                StopCoroutine(coroutine);
                _scareSphereDictionary.Remove(other);

                if (_scareSphereDictionary.Count == 0 && _fearAmount < _data.FearAmountLimit)
                    _isGettingScared = false;
            }
        }

        private void ResetFearAmount()
        {
            _fearAmount = 0f;
            FearAmountChanged?.Invoke(_fearAmount);
        }

        public void ApplyFear(float value)
        {
            _fearAmount += value;

            if (_fearAmount >= _data.FearAmountLimit)
            {
                _fearAmount = _data.FearAmountLimit;
                StephanScared?.Invoke();
            }
        
            FearAmountChanged?.Invoke(value);
        }
    }
}