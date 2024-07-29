using System;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public event Action<Enemy> OnEnemyEnter;
    public event Action<Enemy> OnEnemyExit;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Enemy enemy))
        {
            OnEnemyEnter?.Invoke(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Enemy enemy))
        {
            OnEnemyExit?.Invoke(enemy);
        }
    }
}
