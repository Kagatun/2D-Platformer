using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private AnimationsEnemy _animations;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Collider2D _collider;

    private WaitForSecondsRealtime _wait;
    private float _time = 1.2f;
    private bool _isDead;
    private int _health = 2;

    public float ForcePush { get; private set; } = 2;
    public int Damage { get; private set; } = 1;
    public bool IsLive => _health > 0;

    private void Start()
    {
        _isDead = false;
        _wait = new WaitForSecondsRealtime(_time);
    }

    private void Update()
    {
        if (!IsLive && !_isDead)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _animations.AnimationHit();
    }

    private IEnumerator EnableDeleteTimer()
    {
        _animations.AnimationHit();

        yield return _wait;

        Destroy(gameObject);
    }

    private void Die()
    {
        _isDead = true;
        _collider.enabled = false;
        _rigidbody.velocity = Vector2.zero;

        float forceHit = 0.5f;
        float forceHitUp = 2f;
        float torque = 5f;

        Vector2 damageDirection = transform.position.normalized * forceHit;
        Vector2 forceDirection = Vector2.up * forceHitUp + new Vector2(damageDirection.x, damageDirection.y);

        _rigidbody.constraints = RigidbodyConstraints2D.None;
        _rigidbody.AddForce(forceDirection, ForceMode2D.Impulse);
        _rigidbody.AddTorque(torque, ForceMode2D.Impulse);

        StartCoroutine(EnableDeleteTimer());
    }
}
