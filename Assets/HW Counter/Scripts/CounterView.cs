using TMPro;
using UnityEngine;

public class CounterView : MonoBehaviour
{
    [SerializeField] private Counter _counter;
    [SerializeField] private TMP_Text _counterText;

    private void OnEnable()
    {
        if(_counter != null)
        {
            _counter.CounterChanged += OnValueChanged;
            OnValueChanged(_counter.CounterValue);
        }
    }

    private void OnValueChanged(int value)
    {
        if (_counterText != null)
        {
            _counterText.text = value.ToString();
        }
    }

    private void OnDisable()
    {
        if (_counter != null)
        {
            _counter.CounterChanged += OnValueChanged;
        }
    }
}
