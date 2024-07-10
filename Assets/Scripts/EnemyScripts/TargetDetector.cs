using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    [SerializeField] private TargetSwitchEnemy _targetSwitchEnemy;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Hero hero))
        {
            _targetSwitchEnemy.PursueTarget(collider.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Hero hero))
        {
            _targetSwitchEnemy.Patrolling();
        }
    }
}