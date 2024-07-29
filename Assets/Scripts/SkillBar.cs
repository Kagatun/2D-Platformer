using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour
{
    [SerializeField] private Vampirism _vampirism;
    [SerializeField] private Slider _slider;

    private Coroutine _coroutineSmoothChange;
    private float _smoothTime = 0.5f;
    private float _maxValue = 1f;

    private void Start()
    {
        _vampirism.OnTime += DrawTime;
        _slider.maxValue = _maxValue;
        _slider.value = _maxValue;
    }

    private void OnDisable()
    {
        if (_vampirism != null)
        {
            _vampirism.OnTime -= DrawTime;
        }
    }

    public void DrawTime(int time, int maxTime)
    {
        if (_coroutineSmoothChange != null)
        {
            StopCoroutine(_coroutineSmoothChange);
        }

        float targetValue = (float)time / maxTime;
        _coroutineSmoothChange = StartCoroutine(SmoothTimeChange(targetValue));
    }

    private IEnumerator SmoothTimeChange(float targetValue)
    {
        float startValue = _slider.value;
        float elapsedTime = 0f;

        while (_slider.value != targetValue)
        {
            _slider.value = Mathf.MoveTowards(startValue, targetValue, elapsedTime / _smoothTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _slider.value = targetValue;
    }
}
