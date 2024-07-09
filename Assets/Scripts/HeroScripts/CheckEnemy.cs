using UnityEngine;

public class CheckEnemy : MonoBehaviour
{
    [SerializeField] private MoverCharacter _moverCharacter;
    [SerializeField] private Hero _hero;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Vector2 _raycastOffset;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _raycastDistance;
    [SerializeField] private float _x;
    [SerializeField] private float _y;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((_groundLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            return;
        }

        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            Vector2 raycastOrigin = (Vector2)transform.position + _raycastOffset;

            RaycastHit2D hit = Physics2D.BoxCast(raycastOrigin + (-(Vector2)transform.up) * _raycastDistance, new Vector2(_x, _y), 0f, Vector2.up, _raycastDistance, _enemyLayer);

            if (hit)
            {
                _moverCharacter.JumpAfterImpact();
                enemy.TakeDamage(_hero.Damage);
            }
            else
            {
                _hero.TakeDamage(collision);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 raycastOrigin = (Vector2)transform.position + _raycastOffset;
        Gizmos.DrawWireCube(raycastOrigin + (-(Vector2)transform.up) * _raycastDistance, new Vector2(_x, _y));
    }
}
