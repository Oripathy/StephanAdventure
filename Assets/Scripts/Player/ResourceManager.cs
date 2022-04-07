using System;
using UnityEngine;

internal class ResourceManager : MonoBehaviour
{
    private float _foodAmount;
    
    public event Action<float> FoodTaken;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Food>(out var food))
            FoodTaken?.Invoke(food.FoodAmount);
    }
}