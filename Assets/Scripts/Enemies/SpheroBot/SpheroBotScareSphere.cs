using UnityEngine;

namespace Enemies.SpheroBot
{
    internal class SpheroBotScareSphere : ScareSphere
    {
        [SerializeField] private SpheroBotData _data;

        private protected override void SetFearAmountToReceive() => FearAmountToReceive = _data.FearAmountToReceive;
    }
}