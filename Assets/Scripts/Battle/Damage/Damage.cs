using UnityEngine;

/// <summary>
/// Тип наносимого урона.
/// </summary>
public abstract class Damage : ScriptableObject
{
    [SerializeField] protected TranslatedText _name = null;

    public string Name => _name.Value;

    public abstract float CalculateDamage(Character owner,
                                          Character opponent,
                                          DamageTier tier);

    public abstract float CalculateDamage(Character target,
                                          float baseDamage);
}
