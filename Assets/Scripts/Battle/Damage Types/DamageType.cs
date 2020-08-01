using UnityEngine;

/// <summary>
/// Тип наносимого урона.
/// </summary>
public abstract class DamageType : ScriptableObject
{
    [SerializeField] private string _displayName = "Type";
    /// <summary>
    /// Отображаемый подзаголовок.
    /// </summary>
    public string DisplayName => _displayName;
    /// <summary>
    /// Рассчитать урон по цели.
    /// </summary>
    public abstract float CalculateDamage(float damage, CharacterStats target);
}
