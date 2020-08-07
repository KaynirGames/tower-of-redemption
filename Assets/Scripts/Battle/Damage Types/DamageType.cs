using UnityEngine;

/// <summary>
/// Тип наносимого урона.
/// </summary>
public abstract class DamageType : ScriptableObject
{
    [SerializeField] protected string _name = string.Empty;
    /// <summary>
    /// Наименование типа урона.
    /// </summary>
    public string Name => _name;
    /// <summary>
    /// Рассчитать урон по цели.
    /// </summary>
    public abstract float CalculateDamage(CharacterStats user, CharacterStats target, PowerTier tier);
}
