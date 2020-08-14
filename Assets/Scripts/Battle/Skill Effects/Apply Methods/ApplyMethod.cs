using UnityEngine;

/// <summary>
/// Способ наложения эффекта.
/// </summary>
public abstract class ApplyMethod : ScriptableObject
{
    [SerializeField] protected TranslatedText _nameKey = null;
    /// <summary>
    /// Наименование способа наложения эффекта.
    /// </summary>
    public string Name => _nameKey.Value;
    /// <summary>
    /// Обработать способ наложения эффекта.
    /// </summary>
    public abstract void Handle(CharacterStats target, SkillEffect effect);
}
