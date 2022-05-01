using System.Collections;

namespace Items
{
    internal class FreezeBomb : Item
    {
        private float _freezeDuration;
    
        private void Awake()
        {
            _radius = 5f;
        }


        private protected override IEnumerator StartBeingThrown()
        {
            yield return null;
        }
    }
}