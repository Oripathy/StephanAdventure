namespace Enemies.SpheroBot
{
    internal class SpheroBotScareSphere : ScareSphere
    {
        private protected override void Awake()
        {
            FearAmountToReceive = 1f;
        }
    }
}