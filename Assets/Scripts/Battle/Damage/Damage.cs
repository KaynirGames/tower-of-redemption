using UnityEngine;

/// <summary>
/// Тип наносимого урона.
/// </summary>
public abstract class Damage : ScriptableObject
{
    [SerializeField] protected TranslatedText _name = null;
    [SerializeField] protected Color _textColor = new Color(0, 0, 0, 255);

    public string TextColorHtml { get; private set; }

    public abstract float CalculateDamage(Character owner,
                                          Character opponent,
                                          DamageTier tier);

    public abstract float CalculateDamage(Character target,
                                          float baseDamage);

    public string GetName()
    {
        return string.Format("<color=#{0}><b>{1}</b></color>",
                             TextColorHtml,
                             _name.Value);
    }

    protected virtual void OnValidate()
    {
        TextColorHtml = ColorUtility.ToHtmlStringRGBA(_textColor);
    }
}
