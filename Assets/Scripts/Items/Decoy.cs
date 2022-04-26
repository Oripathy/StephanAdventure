using System;
using System.Collections;
using Enemies;
using UnityEngine;

namespace Items
{
    internal class Decoy : Item
    {
        private void Awake()
        {
            _radius = 10f;
            _delay = 2f;
        }

        private protected override IEnumerator StartBeingThrown()
        {
            _collider.isTrigger = false;
            _rigidbody.isKinematic = false;
            transform.parent = null;
            yield return new WaitForSeconds(_delay);
            var targetsInRange = Physics.OverlapSphere(transform.position, _radius, _enemyLayer);

            foreach (var target in targetsInRange)
            {
                if (target.TryGetComponent<IProvocable>(out var provocable))
                    provocable.Provoke(transform.position);
            }
        }
    }
}