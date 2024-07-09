using UnityEngine;

public class AnimationsEnemy : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string Velocity = "speed";
    private const string Hit = "hit";

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
