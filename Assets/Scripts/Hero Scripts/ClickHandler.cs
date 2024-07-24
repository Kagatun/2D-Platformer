using System.Collections;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private StartPoint _startPoint;
    [SerializeField] private JumpCharacter _jumpCharacter;
    [SerializeField] private MoverCharacter _moverCharacter;

    private WaitForSecondsRealtime _wait;
    private float _shutdownTime = 0.5f;
    private bool _isControlEnabled;

    private void Awake()
    {
        _wait = new WaitForSecondsRealtime(_shutdownTime);
    }

    private void Start()
    {
        Vector3 startPosition = _startPoint.transform.position - new Vector3(0, 0.2f, 0);
        transform.position = startPosition;
        _isControlEnabled = true;
    }

    private void Update()
    {
        if (_isControlEnabled == false)
            return;

        _moverCharacter.Move();
        _jumpCharacter.ToRide();
    }

    public void FreezeControl()
    {
        StartCoroutine(DisableControlTemporarily());
    }

    private IEnumerator DisableControlTemporarily()
    {
        _isControlEnabled = false;

        yield return _wait;

        _isControlEnabled = true;
    }
}
