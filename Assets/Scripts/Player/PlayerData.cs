using UnityEngine;

namespace Player
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerData", order = 1)]
    internal class PlayerData : ScriptableObject
    {
        [SerializeField] private Vector3 _throwDirection;
        [SerializeField] private float _rotationSpeed = 720f;
        [SerializeField] private float _throwForce = 100f;

        public Vector3 ThrowDirection => _throwDirection;
        public float RotationSpeed => _rotationSpeed;
        public float ThrowForce => _throwForce;
    }
}