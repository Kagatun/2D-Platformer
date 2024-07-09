using UnityEngine;

public class CheckGround : MonoBehaviour
{
    [SerializeField] private MoverCharacter _moverCharacter;
    [SerializeField] private Vector2 _raycastOffset;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _raycastDistance;
    [SerializeField] private float _x;
    [SerializeField] private float _y;

    private void FixedUpdate()
    {
        CheckIfOnGround();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 raycastOrigin = (Vector2)transform.position + _raycastOffset;
        Gizmos.DrawWireCube(raycastOrigin + (-(Vector2)transform.up) * _raycastDistance, new Vector2(_x, _y));
    }

    private void CheckIfOnGround()
    {
        Vector2 raycastOrigin = (Vector2)transform.position + _raycastOffset;
        RaycastHit2D hit = Physics2D.BoxCast(raycastOrigin, new Vector2(_x, _y), 0f, -Vector2.up, _raycastDistance, _groundLayer | _enemyLayer);

        if (hit.collider != null)
        {
            if (_enemyLayer == (_enemyLayer | (1 << hit.collider.gameObject.layer)))
            {
                return;
            }
            else if (_groundLayer == (_groundLayer | (1 << hit.collider.gameObject.layer)))
            {
                _moverCharacter.SetGroundedStatus(true);
            }
        }
        else
        {
            _moverCharacter.SetGroundedStatus(false);
        }
    }
}
