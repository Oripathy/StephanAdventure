using System;
using UnityEngine;
using Items;

namespace Player
{
    internal class ResourceManager : MonoBehaviour
    {
        [SerializeField] private Transform _torsoHoldingPoint;
        [SerializeField] private float _foodAmount;

        public Item Item { get; private set; }

        public event Action FoodTaken;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Food>(out var food))
                FoodTaken?.Invoke();

            if (other.TryGetComponent<Item>(out var item))
            {
                if (Item != null)
                    return;

                Item = item;
                Item.OnPickedUp(_torsoHoldingPoint);
            }
        }
    }
}