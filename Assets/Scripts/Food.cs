using UnityEngine;

internal class Food : MonoBehaviour
{
    private float _rotationSpeed;

    public float FoodAmount { get; private set; }

    private void Awake()
    {
        FoodAmount = 0.04f;
        _rotationSpeed = 30f;
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0f, _rotationSpeed, 0f) * Time.deltaTime);
    }
    
}