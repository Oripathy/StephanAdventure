using TMPro;
using UnityEngine;

namespace Enemies.BioBot
{
    internal class BioBotScareSphere : ScareSphere
    {
        [SerializeField] private BioBotData _data;

        private protected override void SetFearAmountToReceive() => FearAmountToReceive = _data.FearAmountToReceive;
    }
}