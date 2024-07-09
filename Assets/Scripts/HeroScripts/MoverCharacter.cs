using System.Collections;
using UnityEngine;

public class MoverCharacter : MonoBehaviour
{
    [SerializeField] private StartPoint _startPoint;
    [SerializeField] private SpriteRenderer _personSprite;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private AnimationsCharacter _animations;

    private const string Horizontal = "Horizontal";
    private WaitForSecondsRealtime _wait;

    private Vector3 _input;
    private float _speed = 2;
    private float _jumpForce = 3;
    private float _shutdownTime = 0.5f;

    private bool _isMoving;
    private bool _isGrounded;
    private bool _isJumpingDouble;
    private bool _isControlEnabled;

    private void Awake()
    {
        _wait = new WaitForSecondsRealtime(_shutdownTime);
    }

    private void Start()
    {
        Vector3 startPosition = _startPoint.transform.position - new Vector3(0, 0.2f, 0);
        transform.position = startPosition;
        _isControlEnabled = true;
    }

    private void Update()
    {
        if (_isControlEnabled)
        {
            Move();
            
            if(_isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                    _animations.TriggerJump();
                    _isJumpingDouble = true;
                }
                else
                {
                    _isJumpingDouble = true;
                }
            }
            else if (_isGrounded == false && _isJumpingDouble && Input.GetKeyDown(KeyCode.Space))
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
                Jump();
                _animations.TriggerJumpDouble();
                _isJumpingDouble = false;
            }
        }
    }

    public void TurnOffGrounding()
    {
        _isGrounded = false;
    }

    public void TurnOnGrounding()
    {
        _isGrounded = true;
    }

    public void JumpAfterImpact()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }

    public void FreezeControl()
    {
        StartCoroutine(DisableControlTemporarily());
    }

    private void Move()
    {
        _input = new Vector2(Input.GetAxis(Horizontal), 0);
        transform.position += _input * _speed * Time.deltaTime;

        _isMoving = _input.x != 0;

        if (_isMoving)
        {
            _personSprite.flipX = _input.x <= 0;
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
        return _rigidbody.velocity.y < 0;
    }

    private IEnumerator DisableControlTemporarily()
    {
        _isControlEnabled = false;

        yield return _wait;

        _isControlEnabled = true;
    }
}

