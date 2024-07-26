using System;
using UnityEngine;

public class Health
{
    public event Action<int, int> OnHealthChanged;
    public event Action OnDie;
    public event Action OnTookDamage;

    public int Value { get; private set; }
    public int MaxValue { get; private set; }

    public Health(int maxHealth)
    {
        Value = maxHealth;
        MaxValue = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        Value -= damage;
        Value = Mathf.Clamp(Value, 0, MaxValue);
        OnHealthChanged?.Invoke(Value, MaxValue);
        OnTookDamage?.Invoke();

        if (Value <= 0)
        {
            OnDie?.Invoke();
        }
    }

    public virtual void Heal(int healingPoints)
    {
        Value += healingPoints;
        Value = Mathf.Clamp(Value, 0, MaxValue);
        OnHealthChanged?.Invoke(Value, MaxValue);
    }
}
