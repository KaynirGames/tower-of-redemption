using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffects : MonoBehaviour
{
    public event Action<EffectInstance> OnDisplayEffectCall = delegate { };

    public List<EffectInstance> StatBonuses { get; private set; }
    public Dictionary<AilmentData, EffectInstance> AilmentEffects { get; private set; }

    private Dictionary<StatType, EffectInstance> _positiveStatBuffs;
    private Dictionary<StatType, EffectInstance> _negativeStatBuffs;

    private void Awake()
    {
        StatBonuses = new List<EffectInstance>();
        AilmentEffects = new Dictionary<AilmentData, EffectInstance>();
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

    public void DisableBattleEffects()
    {
        DisableAilments();
        DisableBuffs(true);
        DisableBuffs(false);
    }

    private void DisableBuffs(bool isPositive)
    {
        var buffs = GetStatBuffs(isPositive);

        if (buffs.Count > 0)
        {
            List<StatType> buffKeys = new List<StatType>(buffs.Keys);

            foreach (var key in buffKeys)
            {
                buffs[key].RemoveEffect();
            }
        }
    }

    private void DisableAilments()
    {
        if (AilmentEffects.Count > 0)
        {
            List<AilmentData> ailmentKeys = new List<AilmentData>(AilmentEffects.Keys);

            foreach (var key in ailmentKeys)
            {
                AilmentEffects[key].RemoveEffect();
            }
        }
    }
}
