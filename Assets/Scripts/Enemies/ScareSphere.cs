using UnityEngine;

namespace Enemies
{
    internal abstract class ScareSphere : MonoBehaviour
    {
        [SerializeField] private protected Collider _collider;

        public float FearAmountToReceive { get; private protected set; }

        private protected abstract void Awake();
    }
}