namespace Enemies.Kyle
{
    internal class KyleScareSphere : ScareSphere
    {
        private protected override void Awake()
        {
            FearAmountToReceive = 1f;
        }
    }
}