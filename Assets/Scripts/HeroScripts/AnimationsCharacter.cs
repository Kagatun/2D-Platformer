using UnityEngine;

public class AnimationsCharacter : MonoBehaviour
{
    private const string Run = "isMoving";
    private const string Fall = "isFlying";
    private const string Hit = "takeDamage";
    private const string Jump = "jump";
    private const string JumpDouble = "jumpDouble";

    [SerializeField] private Animator _animator;

    public bool IsMoving { get; private set; }
    public bool IsFlying { get; private set; }

    private void Update()
    {
        _animator.SetBool(Run, IsMoving);
        _animator.SetBool(Fall, IsFlying);
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
        _animator.SetTrigger(Jump);
    }

    public void TriggerJumpDouble()
    {
        _animator.SetTrigger(JumpDouble);
    }

    public void TriggerTakeDamage()
    {
        _animator.SetTrigger(Hit);
    }
}
