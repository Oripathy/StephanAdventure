using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class FearBehaviour : MonoBehaviour
{
    [SerializeField] private StephanData _data;
    [SerializeField] private Stephan _stephan;

    private Dictionary<Collider, Coroutine> _scareSphereDictionary;
    [SerializeField] private bool _isGettingScared;
    [SerializeField] private float _fearAmount;

    public event Action<float> FearAmountChanged;
    public event Action StephanScared;

    private void Awake()
    {
        _scareSphereDictionary = new Dictionary<Collider, Coroutine>();
        _stephan.StephanFound += ResetFearAmount;
    }

    private void FixedUpdate()
    {
        if (!_isGettingScared && _fearAmount > 0)
            DecreaseFearAmount(0.01f);
    }

    private void DecreaseFearAmount(float value) => _fearAmount -= value;
    
    private IEnumerator IncreaseFearAmount(float value)
    {
        while (_fearAmount < _data.FearAmountLimit)
        {
            _fearAmount += value;
            FearAmountChanged?.Invoke(_fearAmount);
            yield return new WaitForFixedUpdate();
        }
        
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

    private void ResetFearAmount() => _fearAmount = 0f;
}