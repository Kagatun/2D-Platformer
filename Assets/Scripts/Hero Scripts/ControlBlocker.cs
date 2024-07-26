using System.Collections;
using UnityEngine;

public class ControlBlocker : MonoBehaviour
{
    [SerializeField] private InputDetector _inputDetector;
    [SerializeField] private Hero _hero;

    private WaitForSecondsRealtime _wait;
    private float _shutdownTime = 0.3f;

    private void Awake()
    {
        _wait = new WaitForSecondsRealtime(_shutdownTime);
    }

    private void Start()
    {
        _hero.Health.OnTookDamage += FreezeControl;
    }

    private void OnDisable()
    {
        _hero.Health.OnTookDamage -= FreezeControl;
    }

    public void FreezeControl()
    {
        StartCoroutine(DisableControlTemporarily());
    }

    private IEnumerator DisableControlTemporarily()
    {
        _inputDetector.enabled = false;

        yield return _wait;

        _inputDetector.enabled = true;
    }
}
