using System;
using UnityEngine;

public class InputDetector : MonoBehaviour
{
    private const string Horizontal = "Horizontal";

    public event Action OnJumpKey;
    public event Action <float> OnMoveKey;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpKey?.Invoke();
        }

        float horizontalInput = Input.GetAxis(Horizontal);
        OnMoveKey?.Invoke(horizontalInput);
    }
}
