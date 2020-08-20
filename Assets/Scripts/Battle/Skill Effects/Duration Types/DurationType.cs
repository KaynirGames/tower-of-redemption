using UnityEngine;

/// <summary>
/// Тип продолжительности эффекта.
/// </summary>
public abstract class DurationType : ScriptableObject
{
    [SerializeField] protected TranslatedText _typeNameText = null;
    /// <summary>
    /// Наименование типа продолжительности эффекта.
    /// </summary>
    public virtual string TypeName => _typeNameText.Value;
    /// <summary>
    /// Запустить время действия эффекта на цель.
    /// </summary>
    public abstract void Execute(SkillEffect effect, CharacterStats target);
    /// <summary>
    /// Окончить время действия эффекта на цель.
    /// </summary>
    public abstract void Terminate(SkillEffect effect, CharacterStats target);
}
