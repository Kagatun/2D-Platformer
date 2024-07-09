using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private AnimationsCharacter _animations;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private MoverCharacter _moverCharacter;

    private int _maxHealth = 3;
    private int _health;
    private int _numberCoins = 0;
    private float _deadlySpeedFall = -12f;

    public int Damage { get; private set; } = 1;

    private void Start()
    {
        _health = _maxHealth;
    }

    private void Update()
    {
        if (_health <= 0 || _rigidbody.velocity.y < _deadlySpeedFall)
        {
            Destroy(gameObject);
            print("Герой погиб");
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PickUpApple(collider);
        PickUpCoin(collider);
    }

    public void TakeDamage(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            _health -= enemy.Damage;

            _animations.TriggerTakeDamage();
            _moverCharacter.FreezeControl();

            _rigidbody.velocity = Vector2.zero;

            Vector2 damageDirection = transform.position - collision.transform.position;
            damageDirection = damageDirection.normalized * enemy.ForcePush;

            _rigidbody.AddForce(new Vector2(damageDirection.x, damageDirection.y + enemy.ForcePush), ForceMode2D.Impulse);
        }
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
            Destroy(coin.gameObject);
            print("Монетка подобрана. Монетки героя " + _numberCoins);
        }
    }

    private void PickUpApple(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Apple apple))
        {
            RestoreHealth(apple.HealingPoints);
            Destroy(apple.gameObject);
            print("Здоровье восстановлено. Здоровье героя " + _health);
        }
    }
}
