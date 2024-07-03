using UnityEngine;

public class Hero : MonoBehaviour
{
    private int _maxHealth = 100;
    private int _health;
    private int _numberCoins = 0;

    private void Start()
    {
        _health = _maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TakeDamage(collision);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PickUpApple(collider);
        PickUpCoin(collider);
    }

    private void RestoreHealth(int healingPoints)
    {
        _health += healingPoints;

        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }

    private void PickUpCoin(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Coin coin))
        {
            _numberCoins++;
            coin.Destroy();
        }
    }

    private void PickUpApple(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Apple apple))
        {
            RestoreHealth(apple.HealingPoints);
            apple.Destroy();
        }
    }

    private void TakeDamage(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            _health -= enemy.Damage;
        }
    }
}
