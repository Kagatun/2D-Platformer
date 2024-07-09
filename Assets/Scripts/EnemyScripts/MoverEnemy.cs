using UnityEngine;

public class MoverEnemy : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private SpriteRenderer _enemySprite;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private AnimationsEnemy _animations;

    private Transform _target;
    private bool _isMoving;
    private int _currentWaypoint = 0;
    private float _speed = 0.7f;

    public bool IsGotoWaypoint { get; private set; }

    private void Start()
    {
        IsGotoWaypoint = true;
        transform.position = _waypoints[0].position;
    }

    private void Update()
    {
        if (_enemy.IsLive)
        {
            Move();
        }
    }

    public void StartMovementToWaypoint()
    {
        IsGotoWaypoint = true;
    }

    public void StopMovementToWaypoint(Transform target)
    {
        IsGotoWaypoint = false;
        _target = target;
    }

    private void Move()
    {
        float movementSpeed = _rigidbody.velocity.magnitude;

        if (IsGotoWaypoint)
        {
            MoveToNextWaypoint(movementSpeed);
        }
        else
        {
            MoveTowardsTarget(_target.transform.position, movementSpeed);
        }
    }

    private void MoveToNextWaypoint(float movementSpeed)
    {
        float distanceToTargetSqr = 0.01f;

        if ((_waypoints[_currentWaypoint].transform.position - transform.position).sqrMagnitude < distanceToTargetSqr)
        {
            _currentWaypoint = ++_currentWaypoint % _waypoints.Length;
        }

        MoveTowardsTarget(_waypoints[_currentWaypoint].position, movementSpeed);
    }

    private void MoveTowardsTarget(Vector3 target, float movementSpeed)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);

        _isMoving = directionToTarget.x != 0;

        if (_isMoving)
        {
            _enemySprite.flipX = directionToTarget.x >= 0;
        }

        _animations.EnableMotionAnimation(movementSpeed);
    }
}
