using UnityEngine;

namespace Enemies
{
    internal abstract class EnemyData : ScriptableObject
    {
        [SerializeField] private float _movementSpeed = 2f;
        [SerializeField] private float _waitTime = 2f;

        public float MovementSpeed => _movementSpeed;
        public float WaitTime => _waitTime;
    }
}