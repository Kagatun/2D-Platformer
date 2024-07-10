using UnityEngine;

public class HealthCondition : MonoBehaviour
{
    [SerializeField] private AnimationsCharacter _animations;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private ClickHandler _clickHandler;

    private int _maxHealth = 5;
    private int _health;
    private float _deadlySpeedFall = -12f;

    private void Start()
    {
        _health = _maxHealth;
    }

    private void Update()
    {
        if (_health <= 0 || _rigidbody.velocity.y < _deadlySpeedFall)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            _health -= enemy.Damage;

            _animations.TriggerTakeDamage();
            _clickHandler.FreezeControl();

            _rigidbody.velocity = Vector2.zero;

            Vector2 damageDirection = transform.position - collision.transform.position;
            damageDirection = damageDirection.normalized * enemy.ForcePush;

            _rigidbody.AddForce(new Vector2(damageDirection.x, damageDirection.y + enemy.ForcePush), ForceMode2D.Impulse);
        }
    }

    public void RestoreHealth(int healingPoints)
    {
        _health += healingPoints;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
    }
}
