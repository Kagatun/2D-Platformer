using UnityEngine;

public class JumpCharacter : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private AnimationsCharacter _animations;

    private float _jumpForce = 3;

    private bool _isGrounded;
    private bool _isJumpingDouble;

    public void TurnOffGrounding()
    {
        _isGrounded = false;
    }

    public void TurnOnGrounding()
    {
        _isGrounded = true;
    }

    public void SecondJump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        Jump();
    }

    public void ToRide()
    {
        if (_isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FirstJump();
            }
            else
            {
                _isJumpingDouble = true;
            }
        }
        else if (_isGrounded == false && _isJumpingDouble && Input.GetKeyDown(KeyCode.Space))
        {
            JumpAgain();
        }

        _animations.EnableJumpAnimation(GetFlightStatus());
    }

    private bool GetFlightStatus()
    {
        return _rigidbody.velocity.y < 0;
    }

    private void FirstJump()
    {
        Jump();
        _animations.TriggerJump();
        _isJumpingDouble = true;
    }

    private void JumpAgain()
    {
        SecondJump();
        _animations.TriggerJumpDouble();
        _isJumpingDouble = false;
    }

    private void Jump()
    {
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }
}
