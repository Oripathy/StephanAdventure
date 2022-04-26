using UnityEngine;

namespace Enemies.SpheroBot
{
    [CreateAssetMenu(menuName = "ScriptableObjects/SpheroBotData", order = 2)]
    internal class SpheroBotData : ScriptableObject
    {
        [SerializeField] private float _movementSpeed = 2f;

        public float MovementSpeed => _movementSpeed;
    }
}