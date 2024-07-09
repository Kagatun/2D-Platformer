using UnityEngine;

public class JumpAttackerEnemy : MonoBehaviour
{
    [SerializeField] private MoverCharacter _moverCharacter;
    [SerializeField] private Hero _hero;
    [SerializeField] private HealthCondition _healthCondition;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private LayerMask _groundLayer;

    private Vector2 _raycastOffset;
    private float _offsetX = 0f;
    private float _offsetY = -0.1f;
    private float _raycastDistance = 0;
    private float _sizeX = 0.19f;
    private float _sizeY = 0.05f;

    private void Start()
    {
        _raycastOffset = new Vector2(_offsetX, _offsetY);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((_groundLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            return;
        }

        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            Vector2 raycastOrigin = (Vector2)transform.position + _raycastOffset;

            RaycastHit2D hit = Physics2D.BoxCast(raycastOrigin + (-(Vector2)transform.up) * _raycastDistance, new Vector2(_sizeX, _sizeY), 0f, Vector2.up, _raycastDistance, _enemyLayer);

            if (hit)
            {
                _moverCharacter.JumpAfterImpact();
                enemy.TakeDamage(_hero.Damage);
            }
            else
            {
                _healthCondition.TakeDamage(collision);
            }
        }
    }
}
