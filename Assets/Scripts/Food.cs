using UnityEngine;

internal class Food : MonoBehaviour
{
    public float FoodAmount { get; private set; }

    private void Awake()
    {
        FoodAmount = 0.04f;
    }
    
}