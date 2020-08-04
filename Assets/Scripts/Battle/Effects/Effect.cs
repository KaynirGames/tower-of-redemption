using System.Collections;
using UnityEngine;

/// <summary>
/// Эффект, накладываемый на персонажа.
/// </summary>
public abstract class Effect : ScriptableObject
{
    [Header("Базовые параметры эффекта:")]
    [SerializeField] protected EffectType _effectType = EffectType.Positive; // Тип эффекта.
    [SerializeField] protected TargetType _targetType = TargetType.Self; // Цель наложения эффекта.
    [SerializeField] protected bool _hasDuration = false; // Наличие времени действия.
    [SerializeField] protected float _duration = 0f; // Время действия.
    /// <summary>
    /// Тип эффекта.
    /// </summary>
    public EffectType EffectType => _effectType;
    /// <summary>
    /// Цель наложения эффекта.
    /// </summary>
    public TargetType TargetType => _targetType;
    /// <summary>
    /// Применить эффект к цели.
    /// </summary>
    public abstract void Apply(CharacterStats target);
    /// <summary>
    /// Убрать эффект с цели.
    /// </summary>
    public abstract void Remove(CharacterStats target);
    /// <summary>
    /// Получить описание эффекта.
    /// </summary>
    public abstract string GetDescription();
    /// <summary>
    /// Корутина времени действия эффекта.
    /// </summary>
    protected IEnumerator RemoveEffectRoutine(CharacterStats target, float duration)
    {
        yield return new WaitForSeconds(duration);
        Remove(target);
    }
}
