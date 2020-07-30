using UnityEngine;

/// <summary>
/// Обычный магический урон.
/// </summary>
[CreateAssetMenu(fileName = "NewMagicDamage", menuName = "Scriptable Objects/Battle/Damage Types/Magic Damage")]
public class MagicDamage : DamageType
{
    [SerializeField] private MagicElement _magicElement = MagicElement.Fire; // Стихийный элемент урона.

    public override float CalculateDamage(float damage, CharacterStats target)
    {
        ElementEfficacy targetEfficacy = target.GetElementEfficacy(_magicElement);
        float damageTaken = damage * (1 - target.MagicDefence.GetValue() / 100) * (targetEfficacy.EfficacyRate.GetValue() / 100);

        return Mathf.Round(damageTaken);
    }
}
