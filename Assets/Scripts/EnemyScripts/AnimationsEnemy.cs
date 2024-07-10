using UnityEngine;

public class AnimationsEnemy : MonoBehaviour
{
    private static class AnimationParams
    {
        public static readonly int Velocity = Animator.StringToHash("speed");
        public static readonly int Hit = Animator.StringToHash("hit");
    }

    [SerializeField] private Animator _animator;

    public float Speed { get; private set; }

    private void Update()
    {
        _animator.SetFloat(AnimationParams.Velocity, Speed);
    }

    public void EnableMotionAnimation(float speed)
    {
        Speed = speed;
    }

    public void AnimationHit()
    {
        _animator.SetTrigger(AnimationParams.Hit);
    }
}
