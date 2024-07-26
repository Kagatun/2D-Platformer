using UnityEngine;

public class AnimationsCharacter : MonoBehaviour
{
    private static class AnimationParams
    {
        public static readonly int Run = Animator.StringToHash("isMoving");
        public static readonly int Fall = Animator.StringToHash("isFlying");
        public static readonly int Hit = Animator.StringToHash("takeDamage");
        public static readonly int Jump = Animator.StringToHash("jump");
        public static readonly int JumpDouble = Animator.StringToHash("jumpDouble");
    }

    [SerializeField] private Animator _animator;

    public bool IsMoving { get; private set; }
    public bool IsFlying { get; private set; }

    private void Update()
    {
        _animator.SetBool(AnimationParams.Run, IsMoving);
        _animator.SetBool(AnimationParams.Fall, IsFlying);
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
        _animator.SetTrigger(AnimationParams.Jump);
    }

    public void TriggerJumpDouble()
    {
        _animator.SetTrigger(AnimationParams.JumpDouble);
    }

    public void TriggerTakeDamage()
    {
        _animator.SetTrigger(AnimationParams.Hit);
    }
}
