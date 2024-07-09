using UnityEngine;

public class AnimationsEnemy : MonoBehaviour
{
    private const string Velocity = "speed";
    private const string Hit = "hit";

    [SerializeField] private Animator _animator;

    public float Speed { get; private set; }

    private void Update()
    {
        _animator.SetFloat(Velocity, Speed);
    }

    public void EnableMotionAnimation(float speed)
    {
        Speed = speed;
    }

    public void AnimationHit()
    {
        _animator.SetTrigger(Hit);
    }
}
