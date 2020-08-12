using System.Text;
using UnityEngine;

/// <summary>
/// Эффект умения.
/// </summary>
public abstract class SkillEffect : ScriptableObject
{
    [SerializeField] protected ApplyMethod _applyMethod = null;

    /// <summary>
    /// Способ наложения эффекта.
    /// </summary>
    public ApplyMethod ApplyMethod => _applyMethod;

    protected StringBuilder _stringBuilder = new StringBuilder(64, 64); // Для описания.
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
