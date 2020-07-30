using UnityEngine;

/// <summary>
/// Эффект, накладываемый на персонажа.
/// </summary>
public abstract class Effect : ScriptableObject
{
    [SerializeField] protected int _priority = 0;
    /// <summary>
    /// Приоритет наложения эффекта.
    /// </summary>
    public int Priority => _priority;
    /// <summary>
    /// Применить эффект умения к цели.
    /// </summary>
    public abstract void Apply(CharacterStats target);
    /// <summary>
    /// Получить информацию об эффекте для отображения.
    /// </summary>
    public abstract string GetDisplayInfo();
}
