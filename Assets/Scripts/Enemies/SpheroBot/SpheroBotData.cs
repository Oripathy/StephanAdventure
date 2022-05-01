using UnityEngine;

namespace Enemies.SpheroBot
{
    [CreateAssetMenu(menuName = "ScriptableObjects/SpheroBotData", order = 2)]
    internal class SpheroBotData : ScriptableObject
    {
        [SerializeField] private float _movementSpeed = 2f;
        [SerializeField] private float _actionDuration = 4f;
        [SerializeField] private float _fearAmountToReceive = 2f;
        [SerializeField] private float _actionCooldown = 14f;
        
        public float MovementSpeed => _movementSpeed;
        public float ActionDuration => _actionDuration;
        public float FearAmountToReceive => _fearAmountToReceive;
        public float ActionCooldown => _actionCooldown;
    }
}