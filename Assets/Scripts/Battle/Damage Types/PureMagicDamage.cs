using UnityEngine;

/// <summary>
/// Чистый магический урон, игнорирует магическую защиту.
/// </summary>
[CreateAssetMenu(fileName = "NewPureMagicDamage", menuName = "Scriptable Objects/Battle/Damage Types/Pure Magic Damage")]
public class PureMagicDamage : DamageType
{
    [SerializeField] private MagicElement _magicElement = MagicElement.Fire; // Стихийный элемент урона.

    public override float CalculateDamage(float damage, CharacterStats target)
    {
        ElementEfficacy targetEfficacy = target.GetElementEfficacy(_magicElement);
        float damageTaken = damage * (targetEfficacy.EfficacyRate.GetValue() / 100);

        return Mathf.Round(damageTaken);
    }
}
