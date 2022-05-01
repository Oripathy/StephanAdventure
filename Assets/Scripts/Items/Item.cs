using System.Collections;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Rigidbody))]
    internal abstract class Item : MonoBehaviour
    {
        [SerializeField] private protected LayerMask _enemyLayer;
        [SerializeField] private protected Rigidbody _rigidbody;
        [SerializeField] private protected Collider _collider;

        private protected float _radius;
        private protected float _delay;

        public Rigidbody Rigidbody => _rigidbody;

        public void BeThrown()
        {
            StartCoroutine(StartBeingThrown());
        }

        public virtual void OnPickedUp(Transform itemHolder)
        {
            _collider.isTrigger = true;
            _rigidbody.isKinematic = true;
            transform.parent = itemHolder;
            transform.position = itemHolder.position;
        }

        private protected abstract IEnumerator StartBeingThrown();
    }
}