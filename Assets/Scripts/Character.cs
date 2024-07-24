using System;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected int health;
    protected int maxHealth;

    public event Action<int, int> OnHealthChanged;

    protected virtual void Start()
    {
        health = maxHealth;
        OnHealthChanged?.Invoke(health, maxHealth);
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        OnHealthChanged?.Invoke(health, maxHealth);
    }

    public virtual void ReplenishHealth(int healingPoints)
    {
        health += healingPoints;
        health = Mathf.Clamp(health, 0, maxHealth);
        OnHealthChanged?.Invoke(health, maxHealth);
    }
}
