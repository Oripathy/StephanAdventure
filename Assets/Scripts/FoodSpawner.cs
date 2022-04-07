using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

internal class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _foodPrefab;
    [SerializeField] private List<Transform> _foodSpawnPoints;

    private Vector3 _spawnOffset;
    private Transform _previousFoodSpawnPoint;

    private void Awake()
    {
        _spawnOffset = new Vector3(0f, 0.5f, 0f);
    }

    private int SelectSpawnPoint()
    {
        if (_previousFoodSpawnPoint == null)
            return (int)Random.Range(0f, _foodSpawnPoints.Count - 1);

        var index = (int)Random.Range(0f, _foodSpawnPoints.Count - 1);
        
        while (_foodSpawnPoints[index] == _previousFoodSpawnPoint)
            index = (int)Random.Range(0f, _foodSpawnPoints.Count - 1);

        return index;
    }

    public Food SpawnFood()
    {
        var index = SelectSpawnPoint();
        _previousFoodSpawnPoint = _foodSpawnPoints[index];
        return Instantiate(_foodPrefab, _foodSpawnPoints[index].position + _spawnOffset, Quaternion.identity).GetComponent<Food>();
    }
}