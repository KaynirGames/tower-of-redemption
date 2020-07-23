using UnityEngine;

/// <summary>
/// Тип наносимого урона.
/// </summary>
public abstract class DamageType : ScriptableObject
{
    // Список накладываемых эффектов (под вопросом).

    /// <summary>
    /// Рассчитать урон по цели.
    /// </summary>
    public abstract float CalculateDamage(float damage, CharacterStats target);
}
