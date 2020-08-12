using UnityEngine;

/// <summary>
/// Тип наносимого урона.
/// </summary>
public abstract class DamageType : ScriptableObject
{
    [SerializeField] protected TranslatedText _nameKey = null; // Содержит ключ перевода для названия.
    /// <summary>
    /// Наименование типа урона.
    /// </summary>
    public string Name => _nameKey.Value;
    /// <summary>
    /// Рассчитать урон по цели.
    /// </summary>
    public abstract float CalculateDamage(CharacterStats user, CharacterStats target, PowerTier tier);
}
