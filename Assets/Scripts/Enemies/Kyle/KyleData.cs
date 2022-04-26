using UnityEngine;

namespace Enemies.Kyle
{
    [CreateAssetMenu(menuName = "ScriptableObjects/KyleData")]
    internal class KyleData : ScriptableObject
    {
        [SerializeField] private float _movementSpeed = 2f;
        [SerializeField] private float _rotationSpeed = 720f;
        [SerializeField] private float _waitTime = 2f;

        public float MovementSpeed => _movementSpeed;
        public float RotationSpeed => _rotationSpeed;
        public float WaitTime => _waitTime;
    }
}