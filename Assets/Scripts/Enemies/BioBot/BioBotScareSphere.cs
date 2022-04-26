namespace Enemies.BioBot
{
    internal class BioBotScareSphere : ScareSphere
    {
        private protected override void Awake()
        {
            FearAmountToReceive = 2f;
        }
    }
}