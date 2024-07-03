using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationsCharacter : MonoBehaviour
{
    private Animator _animator;

    public bool IsMoving { get; private set; }
    public bool IsFlying { get; private set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("IsMoving", IsMoving);
        _animator.SetBool("IsFlying", IsFlying);
    }

    public void EnableMotionAnimation(bool isMoving)
    {
        IsMoving = isMoving;
    }

    public void EnableJumpAnimation(bool isFlying)
    {
        IsFlying = isFlying;
    }    

    public void TriggerJump()
    {
        _animator.SetTrigger("Jump");
    }

    public void TriggerJumpDouble()
    {
        _animator.SetTrigger("JumpDouble");
    }
}
