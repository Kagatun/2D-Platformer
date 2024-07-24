using UnityEngine;

public class Hero : Character
{
    [SerializeField] private AnimationsCharacter _animations;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private ClickHandler _clickHandler;

    private int _numberCoins = 0;
    private float _deadlySpeedFall = -12f;

    public int Damage { get; private set; } = 1;

    protected override void Start()
    {
        maxHealth = 5;
        base.Start();
    }

    private void Update()
    {
        if (health <= 0 || _rigidbody.velocity.y < _deadlySpeedFall)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PickUpApple(collider);
        PickUpCoin(collider);
    }

    public void TakeDamage(int damage, Collision2D collision)
    {
        base.TakeDamage(damage);
        _animations.TriggerTakeDamage();
        _clickHandler.FreezeControl();

        int forcePush = 2;
        _rigidbody.velocity = Vector2.zero;

        Vector2 damageDirection = transform.position - collision.transform.position;
        damageDirection = damageDirection.normalized * forcePush;

        _rigidbody.AddForce(new Vector2(damageDirection.x, damageDirection.y + forcePush), ForceMode2D.Impulse);
    }

    public override void ReplenishHealth(int healingPoints)
    {
        base.ReplenishHealth(healingPoints);
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
            ReplenishHealth(apple.HealingPoints);
            Destroy(apple.gameObject);
        }
    }
}
