using UnityEngine;

namespace Stephan
{
    [CreateAssetMenu(menuName = "ScriptableObjects/StephanData", order = 3)]
    internal class StephanData : ScriptableObject
    {
        [SerializeField] private float _fearAmountLimit = 100f;
        [SerializeField] private float _maxAddedSize = 0.4f;
        [SerializeField] private float _sizeStep = 0.04f;
        [SerializeField] private float _fearAmountDecrease = 1f;

        public float FearAmountLimit => _fearAmountLimit;
        public float SizeStep => _sizeStep;
        public float MaxAddedSize => _maxAddedSize;
        public float FearAmountDecrease => _fearAmountDecrease;
    }
}