using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(AnimationsCharacter))]
public class MoverCharacter : MonoBehaviour
{
    [SerializeField] private StartPoint _startPoint;
    [SerializeField] private SpriteRenderer _personSprite;

    private Rigidbody2D _rigidbody;
    private AnimationsCharacter _animations;

    private Vector3 _input;
    private float _speed = 2;
    private float _jumpForce = 3;

    private bool _isMoving;
    private bool _isGrounded;
    private bool _isJumpingDouble;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animations = GetComponentInChildren<AnimationsCharacter>();
    }

    private void Start()
    {
        Vector3 startPosition = _startPoint.transform.position - new Vector3(0, 0.2f, 0);
        transform.position = startPosition;
    }

    private void Update()
    {
        Move();

        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _animations.TriggerJump();
            Jump();
            _isJumpingDouble = true;
        }
        else if (_isJumpingDouble && Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            Jump();
            _animations.TriggerJumpDouble();
            _isJumpingDouble = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground _))
        {
            _isGrounded = true;
            _isJumpingDouble = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground _))
        {
            _isGrounded = false;
            _isJumpingDouble = true;
        }
    }

    private void Move()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), 0);
        transform.position += _input * _speed * Time.deltaTime;
        _isMoving = _input.x != 0 ? true : false;

        if (_isMoving)
        {
            _personSprite.flipX = _input.x > 0 ? false : true;
        }

        _animations.EnableMotionAnimation(_isMoving);
        _animations.EnableJumpAnimation(ReturnFlightStatus());
    }

    private void Jump()
    {
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }

    private bool ReturnFlightStatus()
    {
        if (_rigidbody.velocity.y < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

