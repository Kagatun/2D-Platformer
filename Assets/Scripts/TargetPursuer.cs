using UnityEngine;

public class TargetPursuer : MonoBehaviour
{
    [SerializeField] private Transform _transformTarget;
    [SerializeField] private float _offsetY;

    private void Update()
    {
        if (_transformTarget != null)
        {
            transform.position = _transformTarget.transform.position + new Vector3(0, _offsetY, 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
