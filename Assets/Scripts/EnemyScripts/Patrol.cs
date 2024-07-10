using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private AnimationsEnemy _animations;
    [SerializeField] private SpriteRenderer _enemySprite;
    [SerializeField] private Rigidbody2D _rigidbody;

    private bool _isMoving;
    private float _speed = 0.7f;
    private int _currentWaypoint = 0;

    private void Start()
    {
        transform.position = _waypoints[0].position;
    }

    private void Update()
    {
        float movementSpeed = _rigidbody.velocity.magnitude;
        float distanceToTargetSqr = 0.01f;

        if ((_waypoints[_currentWaypoint].transform.position - transform.position).sqrMagnitude < distanceToTargetSqr)
        {
            _currentWaypoint = ++_currentWaypoint % _waypoints.Length;
        }

        Vector3 directionToTarget = (_waypoints[_currentWaypoint].transform.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentWaypoint].transform.position, _speed * Time.deltaTime);

        _isMoving = directionToTarget.x != 0;

        if (_isMoving)
        {
            _enemySprite.flipX = directionToTarget.x >= 0;
        }

        _animations.EnableMotionAnimation(movementSpeed);
    }


}
