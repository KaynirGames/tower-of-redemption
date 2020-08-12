using UnityEngine;

/// <summary>
/// Способ наложения эффекта.
/// </summary>
public abstract class ApplyMethod : ScriptableObject
{
    [SerializeField] protected string _name = string.Empty;
    /// <summary>
    /// Наименование способа наложения эффекта.
    /// </summary>
    public string Name => _name;
    /// <summary>
    /// Обработать способ наложения эффекта.
    /// </summary>
    public abstract void Handle(CharacterStats target, SkillEffect effect);
}
