using System.Collections.Generic;
using UnityEngine;

public abstract class StatBuffType : ScriptableObject
{
    [SerializeField] protected StatType _statType = StatType.Strength;
    [SerializeField] protected TranslatedText _descriptionTextFormat = null;
    [SerializeField] protected List<StatBuffType> _incompatibleBuffTypes = null;

    public StatType StatType => _statType;

    public abstract StatModifier CreateStatModifier(float buffPower, CharacterStats stats);

    public string GetDescription(string tierName, float duration, TargetType targetType)
    {
        return targetType == TargetType.Self
            ? string.Format(_descriptionTextFormat.Value,
                            tierName,
                            GameTexts.Instance.TargetSelfLabel,
                            duration)
            : string.Format(_descriptionTextFormat.Value,
                            tierName,
                            GameTexts.Instance.TargetEnemyLabel,
                            duration);
    }

    public bool IsIncompatible(StatBuffType buffType)
    {
        return _incompatibleBuffTypes.Contains(buffType);
    }
}
