using UnityEngine;

public class MoverHealthBarSmooth : MonoBehaviour
{
    [SerializeField] private Transform _transformTarget;

    private void Update()
    {
        if (_transformTarget != null)
        {
            transform.position = _transformTarget.transform.position + new Vector3(0, 0.17f, 0);
        }

        if (_transformTarget == null)
        {
            Destroy(gameObject);
        }
    }
}
