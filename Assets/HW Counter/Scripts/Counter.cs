using System;
using System.Collections;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    [Header("Настройки счетчика")]
    [SerializeField] private float _interval = 0.5f;

    private int _counterValue = 0;
    private bool _isCounting = false;
    private Coroutine _counterCoroutine;

    public int CounterValue => _counterValue;
    public event Action<int> CounterChanged;

    private void OnEnable()
    {
        _inputReader.ButtonCliked += ToggleCounter;
    }
    
    private void ToggleCounter()
    {
        if (_isCounting)
        {
            StopCounting();
        }
        else
        {
            StartCounting();
        }
    }

    private void StartCounting()
    {
        if (_isCounting == true)
            return;

        _isCounting = true;
        _counterCoroutine = StartCoroutine(CountRoutine());
    }

    private void StopCounting()
    {
        _isCounting = false;

        if (_counterCoroutine != null)
        {
            StopCoroutine(_counterCoroutine);
            _counterCoroutine = null;
        }
    }

    private IEnumerator CountRoutine()
    {
        while (_isCounting)
        {
            yield return new WaitForSeconds(_interval);

            _counterValue++;
            CounterChanged?.Invoke(_counterValue);
        }
    }

    private void OnDisable()
    {
        _inputReader.ButtonCliked -= ToggleCounter;
    }
}
