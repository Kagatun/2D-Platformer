using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private MoverCharacter _moverCharacter;
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

    private void FixedUpdate()
    {
        CheckIfOnGround();
    }

    private void CheckIfOnGround()
    {
        Vector2 raycastOrigin = (Vector2)transform.position + _raycastOffset;
        RaycastHit2D hit = Physics2D.BoxCast(raycastOrigin, new Vector2(_sizeX, _sizeY), 0f, -Vector2.up, _raycastDistance, _groundLayer | _enemyLayer);

        if (hit.collider != null)
        {
            if (_enemyLayer == (_enemyLayer | (1 << hit.collider.gameObject.layer)))
            {
                return;
            }
            else if (_groundLayer == (_groundLayer | (1 << hit.collider.gameObject.layer)))
            {
                _moverCharacter.TurnOnGrounding();
            }
        }
        else
        {
            _moverCharacter.TurnOffGrounding();
        }
    }
}
