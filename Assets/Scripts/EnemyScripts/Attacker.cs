using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private MoverEnemy _moverEnemy;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Hero hero))
        {
            _moverEnemy.GoToWaypoint(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Hero hero))
        {
            _moverEnemy.GoToWaypoint(true);
        }
    }
}