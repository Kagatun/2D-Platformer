using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private HealthCondition _healthCondition;

    private int _numberCoins = 0;

    public int Damage { get; private set; } = 1;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PickUpApple(collider);
        PickUpCoin(collider);
    }

    private void PickUpCoin(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Coin coin))
        {
            _numberCoins++;
            Destroy(coin.gameObject);
        }
    }

    private void PickUpApple(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Apple apple))
        {
            _healthCondition.RestoreHealth(apple.HealingPoints);
            Destroy(apple.gameObject);
        }
    }
}
