using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffects : MonoBehaviour
{
    public event Action<Effect> OnDisplayEffectCall = delegate { };

    public List<Effect> StatBonuses { get; private set; }
    public Dictionary<AilmentSO, Effect> AilmentEffects { get; private set; }

    private Dictionary<StatType, Effect> _positiveStatBuffs;
    private Dictionary<StatType, Effect> _negativeStatBuffs;

    private void Awake()
    {
        StatBonuses = new List<Effect>();
        AilmentEffects = new Dictionary<AilmentSO, Effect>();
        _positiveStatBuffs = new Dictionary<StatType, Effect>();
        _negativeStatBuffs = new Dictionary<StatType, Effect>();
    }

    public Dictionary<StatType, Effect> GetStatBuffs(bool isPositive)
    {
        return isPositive
            ? _positiveStatBuffs
            : _negativeStatBuffs;
    }

    public void DisplayEffect(Effect instance)
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
            List<AilmentSO> ailmentKeys = new List<AilmentSO>(AilmentEffects.Keys);

            foreach (var key in ailmentKeys)
            {
                AilmentEffects[key].RemoveEffect();
            }
        }
    }
}
