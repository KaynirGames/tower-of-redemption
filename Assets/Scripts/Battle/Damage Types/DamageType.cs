using UnityEngine;

/// <summary>
/// Тип наносимого урона.
/// </summary>
public abstract class DamageType : ScriptableObject
{
    [SerializeField] private string _name = "Undefined";
    // Список накладываемых эффектов (под вопросом).

    /// <summary>
    /// Наименование типа урона.
    /// </summary>
    public string Name => _name;
    /// <summary>
    /// Рассчитать урон по цели.
    /// </summary>
    public abstract float CalculateDamage(float damage, CharacterStats target);
}
