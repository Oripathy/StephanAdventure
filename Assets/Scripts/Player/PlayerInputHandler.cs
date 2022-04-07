using System;
using UnityEngine;

internal class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    
    [SerializeField] private Vector3 _moveDirection;

    public Vector3 MoveDirection => _moveDirection;

    public event Action AttackButtonPressed;
    public event Action AbilityButtonPressed;

    private void Awake()
    {
        _moveDirection = new Vector3(0f, 0f, 0f);
    }

    private void Update()
    {
        _moveDirection.x = Input.GetAxis("Horizontal");
        _moveDirection.z = Input.GetAxis("Vertical");
        
        if (_moveDirection.magnitude > 1)
            _moveDirection.Normalize();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            AttackButtonPressed?.Invoke();
        
        if (Input.GetKeyDown(KeyCode.Mouse1))
            AbilityButtonPressed?.Invoke();
    }
}
