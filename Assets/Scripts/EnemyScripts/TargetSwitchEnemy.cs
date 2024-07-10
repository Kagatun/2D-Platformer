using UnityEngine;

public class TargetSwitchEnemy : MonoBehaviour
{
    [SerializeField] private Patrol _patrol;
    [SerializeField] private Chase _chase;

    public void Patrolling()
    {
        _patrol.enabled = true;
        _chase.enabled = false;
    }

    public void PursueTarget(Transform newTarget)
    {
        _patrol.enabled = false;
        _chase.enabled = true;
        _chase.GetTarget(newTarget);
    }
}
