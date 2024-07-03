using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(AnimationsEnemy))]
public class MoverEnemy : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private SpriteRenderer _enemySprite;

    private AnimationsEnemy _animations;
    private bool _isMoving;
    private int _currentWaypoint = 0;
    private float _speed = 0.6f;

    private void Start()
    {
        _animations = GetComponentInChildren<AnimationsEnemy>();
        transform.position = _waypoints[0].position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float distancesToTouchWaypointSqr = 0.1f * 0.1f;

        if ((_waypoints[_currentWaypoint].transform.position - transform.position).sqrMagnitude < distancesToTouchWaypointSqr)
        {
            _currentWaypoint = ++_currentWaypoint % _waypoints.Length;
        }

        Vector3 directionToWaypoint = (_waypoints[_currentWaypoint].position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentWaypoint].position, _speed * Time.deltaTime);

        _isMoving = directionToWaypoint.x != 0;

        if (_isMoving)
        {
            _enemySprite.flipX = directionToWaypoint.x < 0 ? false : true;
        }

        _animations.EnableMotionAnimation(_isMoving);
    }
}
