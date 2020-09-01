using System;
using UnityEngine;

[Serializable]
public class CharacterResource
{
    public delegate void OnResourceChange(float currentValue);

    public event OnResourceChange OnChange = delegate { };

    [SerializeField] private Stat _maxValue;

    public Stat MaxValue => _maxValue;

    public float CurrentValue { get; private set; }

    public CharacterResource(float maxValue, float currentValue)
    {
        _maxValue = new Stat(maxValue);
        CurrentValue = currentValue;
    }

    public void ChangeResource(float amount)
    {
        CurrentValue += amount;
        FixCurrentValue();    
    }

    public void FixCurrentValue()
    {
        CurrentValue = Mathf.Round(CurrentValue);
        CurrentValue = Mathf.Clamp(CurrentValue,
                                   0,
                                   _maxValue.GetFinalValue());

        OnChange.Invoke(CurrentValue);
    }
}
