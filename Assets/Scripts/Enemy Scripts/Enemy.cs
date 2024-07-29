using System.Collections;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private AnimationsEnemy _animations;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Collider2D _collider;

    private WaitForSecondsRealtime _wait;
    private float _time = 2f;

    public int Damage { get; private set; } = 25;

    private void Start()
    {
        _wait = new WaitForSecondsRealtime(_time);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        _animations.AnimationHit();
    }

    protected override void Die()
    {
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

    private IEnumerator EnableDeleteTimer()
    {
        _animations.AnimationHit();

        yield return new WaitForSecondsRealtime(_time);

        Destroy(gameObject);
    }
}
