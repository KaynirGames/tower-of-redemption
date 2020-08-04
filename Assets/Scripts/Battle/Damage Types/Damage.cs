using UnityEngine;

/// <summary>
/// Наносимый урон.
/// </summary>
public abstract class Damage : ScriptableObject
{
    /// <summary>
    /// Рассчитать урон по цели.
    /// </summary>
    public abstract float CalculateDamage(CharacterStats target);
    /// <summary>
    /// Получить описание наносимого урона.
    /// </summary>
    public abstract string GetDescription();
}