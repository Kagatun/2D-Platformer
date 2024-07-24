using UnityEngine;

public class MoverCharacter : MonoBehaviour
{
    private const string Horizontal = "Horizontal";

    [SerializeField] private SpriteRenderer _personSprite;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private AnimationsCharacter _animations;

    private Vector3 _input;
    private float _speed = 2;
    private bool _isMoving;

    public void Move()
    {
        _input = new Vector2(Input.GetAxis(Horizontal), 0);
        transform.position += _input * _speed * Time.deltaTime;

        _isMoving = _input.x != 0;

        if (_isMoving)
        {
            _personSprite.flipX = _input.x <= 0;
        }

        _animations.EnableMotionAnimation(_isMoving);
        _animations.EnableJumpAnimation(GetFlightStatus());
    }

    private bool GetFlightStatus()
    {
        return _rigidbody.velocity.y < 0;
    }
}

