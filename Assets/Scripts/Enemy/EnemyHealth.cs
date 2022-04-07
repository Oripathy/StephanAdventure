using UnityEngine;

internal abstract class EnemyHealth : MonoBehaviour
{
    [SerializeField] private protected float _health;
    private protected float _maxHealth;
    public abstract void TakeDamage(float damage);
}