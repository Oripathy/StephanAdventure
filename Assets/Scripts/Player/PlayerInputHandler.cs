using System;
using System.Collections.Generic;
using UnityEngine;
using StageController;

namespace Player
{
    internal class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private Vector3 _moveDirection;
        [SerializeField] private StageSwitcher _stageSwitcher;

        private bool _isUIInputActive;
        
        public Vector3 MoveDirection => _moveDirection;

        public event Action ThrowButtonPressed;
        public event Action AbilityButtonPressed;
        public event Action MouseButtonPressed;

        private void Awake()
        {
            _moveDirection = new Vector3(0f, 0f, 0f);
            _stageSwitcher.GameEnded += SetUIInputActive;
        }

        private void Update()
        {
            if (!_isUIInputActive)
            {
                _moveDirection.x = Input.GetAxis("Horizontal");
                _moveDirection.z = Input.GetAxis("Vertical");

                if (_moveDirection.magnitude > 1)
                    _moveDirection.Normalize();

                if (Input.GetKeyDown(KeyCode.Mouse0))
                    ThrowButtonPressed?.Invoke();

                if (Input.GetKeyDown(KeyCode.Mouse1))
                    AbilityButtonPressed?.Invoke();
            }
            else
            {
                if (Input.GetMouseButton(0))
                    MouseButtonPressed?.Invoke();
            }

        }

        private void SetUIInputActive() => _isUIInputActive = true;
    }
}
