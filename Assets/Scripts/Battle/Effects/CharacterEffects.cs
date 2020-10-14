using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffects : MonoBehaviour
{
    public event Action<EffectInstance> OnDisplayEffectCall = delegate { };

    public List<EffectInstance> StatBonuses { get; private set; }

    private Dictionary<StatType, EffectInstance> _positiveStatBuffs;
    private Dictionary<StatType, EffectInstance> _negativeStatBuffs;

    private void Awake()
    {
        StatBonuses = new List<EffectInstance>();
        _positiveStatBuffs = new Dictionary<StatType, EffectInstance>();
        _negativeStatBuffs = new Dictionary<StatType, EffectInstance>();
    }

    public Dictionary<StatType, EffectInstance> GetStatBuffs(bool isPositive)
    {
        return isPositive
            ? _positiveStatBuffs
            : _negativeStatBuffs;
    }

    public void DisplayEffect(EffectInstance instance)
    {
        OnDisplayEffectCall.Invoke(instance);
    }
}
