using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text _counterText;
    [SerializeField] private Button _button;

    [Header("Настройки счетчика")]
    [SerializeField] private float _interval = 0.5f;

    private int _counter = 0;
    private bool _isRunning = false;
    private Coroutine _counterCorotine;

    private void OnEnable()
    {
        _button.onClick.AddListener(ToggleCounter);
    }

    private void Start()
    {
        UpdateText();
    }

    private void OnDisable()
    {
        if (_button != null)
        {
            _button.onClick.RemoveListener(ToggleCounter);
        }
    }

    private void ToggleCounter()
    {
        if (_isRunning)
        {
            StopCounter();
        }
        else
        {
            StartCounter();
        }
    }

    private void StartCounter()
    {
        _isRunning = true;
        _counterCorotine = StartCoroutine(CountRoutine());
    }

    private void StopCounter()
    {
        _isRunning = false;

        if (_counterCorotine != null)
        {
            StopCoroutine(_counterCorotine);
            _counterCorotine = null;
        }
    }

    private IEnumerator CountRoutine()
    {
        while (_isRunning)
        {
            yield return new WaitForSeconds(_interval);

            _counter++;
            UpdateText();
            Debug.Log("Счётчик: " + _counter);
        }
    }

    private void UpdateText()
    {
        if (_counterText  != null)
        {
            _counterText.text = _counter.ToString();
        }
    }
}
