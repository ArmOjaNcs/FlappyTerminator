using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private protected Health Health;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _currentSmoothDuration;

    private float _smoothDuration;
    private float _startValue;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        Health.HealthUpdate += OnHealthUpdate;
    }

    private void OnDisable()
    {
        Health.HealthUpdate -= OnHealthUpdate;
    }

    private void Start()
    {
        _smoothDuration = _currentSmoothDuration;
        _slider.value = Health.CurrentValue / Health.MaxValue;
    }

    private IEnumerator UpdateView()
    {
        _startValue = _slider.value;
        float elapsedTime = 0;
        float targetValue = Health.CurrentValue / Health.MaxValue;

        while (elapsedTime < _smoothDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedPosition = elapsedTime / _smoothDuration;
            _slider.value = Mathf.Lerp(_startValue, targetValue, normalizedPosition);

            yield return null;
        }

        _slider.value = targetValue;
    }

    private void OnHealthUpdate()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(UpdateView());
    }
}