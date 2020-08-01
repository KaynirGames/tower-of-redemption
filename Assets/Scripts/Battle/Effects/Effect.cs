using UnityEngine;

/// <summary>
/// Эффект, накладываемый на персонажа.
/// </summary>
public abstract class Effect : ScriptableObject
{
    [Header("Базовые параметры эффекта:")]
    [SerializeField] protected EffectType _effectType = EffectType.StatModify; // Тип эффекта.
    [SerializeField] protected TargetType _targetType = TargetType.Self; // Цель наложения эффекта.
    /// <summary>
    /// Тип эффекта.
    /// </summary>
    public EffectType EffectType => _effectType;
    /// <summary>
    /// Цель наложения эффекта.
    /// </summary>
    public TargetType TargetType => _targetType;
    /// <summary>
    /// Приоритет наложения эффекта.
    /// </summary>
    public int Priority => (int)_effectType;
    /// <summary>
    /// Применить эффект к цели.
    /// </summary>
    public abstract void ApplyEffect(CharacterStats target);
    /// <summary>
    /// Получить информацию об эффекте для отображения.
    /// </summary>
    public abstract string GetDisplayInfo();
}
