using UnityEngine;

namespace Enemies
{
    internal abstract class ScareSphere : MonoBehaviour
    {
        public float FearAmountToReceive { get; private protected set; }

        private void Awake()
        {
            SetFearAmountToReceive();
        }

        private protected abstract void SetFearAmountToReceive();
    }
}