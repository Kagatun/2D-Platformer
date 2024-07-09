using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private MoverEnemy _moverEnemy;

    private Transform _target;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Hero hero))
        {
            _target = collider.transform;

            _moverEnemy.StopMovementToWaypoint(_target.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Hero hero))
        {
            _moverEnemy.StartMovementToWaypoint();
            _target = null;
        }
    }
}