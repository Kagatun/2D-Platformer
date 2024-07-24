using UnityEngine;

public class Chase : MonoBehaviour
{
    [SerializeField] private AnimationsEnemy _animations;
    [SerializeField] private SpriteRenderer _enemySprite;
    [SerializeField] private Rigidbody2D _rigidbody;

    private bool _isMoving;
    private float _speed = 0.7f;

    public Transform Target { get; private set; }

    private void Update()
    {
        if (Target != null)
        {
            float movementSpeed = _rigidbody.velocity.magnitude;

            Vector3 directionToTarget = (Target.transform.position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, _speed * Time.deltaTime);

            _isMoving = directionToTarget.x != 0;

            if (_isMoving)
            {
                _enemySprite.flipX = directionToTarget.x >= 0;
            }

            _animations.EnableMotionAnimation(movementSpeed);
        }
    }

    public void GetTarget(Transform target)
    {
        Target = target;
    }
}
