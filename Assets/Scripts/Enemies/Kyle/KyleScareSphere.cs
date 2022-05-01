using UnityEngine;

namespace Enemies.Kyle
{
    internal class KyleScareSphere : ScareSphere
    {
        [SerializeField] private KyleData _data;

        private protected override void SetFearAmountToReceive() => FearAmountToReceive = _data.FearAmountToReceive;
    }
}