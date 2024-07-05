using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ValueViewHolder : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _valueView;
    [SerializeField] private int _minValue;
    [SerializeField] private int _maxValue;
    [SerializeField] private int _startValue;
    [SerializeField] private Button _increase;
    [SerializeField] private Button _decrease;

    public Action<int> OnValueChanget;

    private int Value = 1;

    public int CurrentValue => Value;

    public void Configure()
    {
        _increase.onClick.AddListener(Increase);
        _decrease.onClick.AddListener(Decrease);
        UpdateView();
    }

    private void Decrease()
    {
        Value -= 1;
        UpdateView();
    }

    private void Increase()
    {
        Value += 1;
        UpdateView();
    }

    private void UpdateView()
    {
        Value = Mathf.Clamp(Value, _minValue, _maxValue);
        OnValueChanget?.Invoke(CurrentValue);
        _valueView.text = Value.ToString();
    }
}
