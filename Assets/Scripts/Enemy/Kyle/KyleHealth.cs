using System;

internal class KyleHealth : EnemyHealth
{
    private void Awake()
    {
        _maxHealth = 100;
        _health = _maxHealth;
    }

    public override void TakeDamage(float damage)
    {
        if (damage <= 0)
            return;

        _health -= damage;
    }
}