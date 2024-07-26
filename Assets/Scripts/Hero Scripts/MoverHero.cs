using UnityEngine;

public class MoverHero : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _personSprite;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private AnimationsCharacter _animations;
    [SerializeField] private InputDetector _inputDetector;

    private Vector3 _input;
    private float _speed = 2;
    private bool _isMoving;

    private void OnEnable()
    {
        _inputDetector.OnMoveKey += Move;
    }

    private void OnDisable()
    {
        _inputDetector.OnMoveKey -= Move;
    }

    private void Move(float horizontalInput)
    {
        _input = new Vector2(horizontalInput, 0);
        transform.position += _input * _speed * Time.deltaTime;

        _isMoving = horizontalInput != 0;

        if (_isMoving)
        {
            _personSprite.flipX = horizontalInput < 0;
        }

        _animations.EnableMotionAnimation(_isMoving);
        _animations.EnableJumpAnimation(GetFlightStatus());
    }

    private bool GetFlightStatus()
    {
        return _rigidbody.velocity.y < 0;
    }
}

