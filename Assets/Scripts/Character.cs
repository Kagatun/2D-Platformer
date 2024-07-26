using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private int _maxHealthValue;

    public Health Health { get; private set; }

    private void Awake()
    {
        Health = new Health(_maxHealthValue);
    }

    private void OnEnable()
    {
        Health.OnDie += Die;
    }

    private void OnDestroy()
    {
        Health.OnDie -= Die;
    }

    public virtual void TakeDamage(int damage)
    {
        Health.TakeDamage(damage);
    }

    public void Heal(int heal)
    {
        Health.Heal(heal);
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
