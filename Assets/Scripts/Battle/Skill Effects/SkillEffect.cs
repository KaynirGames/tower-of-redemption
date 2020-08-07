using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Эффект умения.
/// </summary>
public abstract class SkillEffect : ScriptableObject
{
    [SerializeField] protected ApplyType _applyType = ApplyType.Update;
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
}
