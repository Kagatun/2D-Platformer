using UnityEngine;

public class JumperHero : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private AnimationsCharacter _animations;
    [SerializeField] private InputDetector _inputDetector;

    private float _jumpForce = 3;
    private int _jumpCount = 0;
    private int _jumpCountMax = 2;
    private bool _isGrounded;

    private void OnEnable()
    {
        _inputDetector.OnJumpKey += Jump;
    }

    private void OnDisable()
    {
        _inputDetector.OnJumpKey -= Jump;
    }

    public void TurnOffGrounding()
    {
        _isGrounded = false;
    }

    public void TurnOnGrounding()
    {
        _isGrounded = true;
        _jumpCount = 0;
    }

    public void SecondJump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        _jumpCount = _jumpCountMax;
        FirstJump();
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            FirstJump();
        }
        else
        {
            _jumpCount = Mathf.Max(_jumpCount, 1);

            if (_jumpCount < _jumpCountMax)
            {
                SecondJump();
                _animations.TriggerJumpDouble();
            }

            _animations.EnableJumpAnimation(GetFlightStatus());
        }

        _animations.EnableJumpAnimation(GetFlightStatus());
    }

    private bool GetFlightStatus()
    {
        return _rigidbody.velocity.y < 0;
    }

    private void FirstJump()
    {
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
        _animations.TriggerJump();
    }
}
