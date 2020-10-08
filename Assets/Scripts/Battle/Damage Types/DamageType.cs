using UnityEngine;

/// <summary>
/// Тип наносимого урона.
/// </summary>
public abstract class DamageType : ScriptableObject
{
    [SerializeField] protected TranslatedText _name = null;

    public string Name => _name.Value;

    public abstract float CalculateDamage(CharacterStats ownerStats,
                                          CharacterStats opponentStats,
                                          float attackPower);
}
