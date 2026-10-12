using System;
using UnityEngine;
using UnityEngine.UI;

public class InputReader : MonoBehaviour
{
    [SerializeField] private Button _button;

    public event Action ButtonCliked;

    private void OnEnable()
    {
        if (_button != null)
        {
            _button.onClick.AddListener(OnButtonClicked);
        }
    }

    private void OnButtonClicked()
    {
        ButtonCliked?.Invoke();
    }

    private void OnDisable()
    {
        if (_button != null)
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }
    }
}
