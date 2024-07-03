using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationsEnemy : MonoBehaviour
{
    private Animator _animator;

    public bool IsMoving { get; private set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("IsMoving", IsMoving);
    }

    public void EnableMotionAnimation(bool isMoving)
    {
        IsMoving = isMoving;
    }
}
