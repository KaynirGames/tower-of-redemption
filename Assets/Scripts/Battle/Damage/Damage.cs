using UnityEngine;

/// <summary>
/// Тип наносимого урона.
/// </summary>
public abstract class Damage : ScriptableObject
{
    [SerializeField] protected TranslatedText _name = null;
    [SerializeField] protected TextColorData _textColorData = null;

    public abstract float CalculateDamage(Character owner,
                                          Character opponent,
                                          DamageTier tier);

    public abstract float CalculateDamage(Character target,
                                          float baseDamage);

    public string GetDamageName()
    {
        return _textColorData.ColorText(_name.Value);
    }
}
