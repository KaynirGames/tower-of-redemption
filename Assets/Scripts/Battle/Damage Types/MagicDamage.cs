using UnityEngine;

/// <summary>
/// Магический урон.
/// </summary>
[CreateAssetMenu(fileName = "NewMagicDamage", menuName = "Scriptable Objects/Battle/Damage Types/Magic Damage")]
public class MagicDamage : DamageType
{
    [SerializeField] private ElementType _elementType = ElementType.Fire;

    public override float CalculateDamage(CharacterStats user, CharacterStats target, PowerTier tier)
    {
        float userPower = user.Will.GetValue() * tier.PowerModifier;
        float targetDefence = 1 - (target.Defence.GetValue() / 100);
        float efficacyModifier = 1 - target.GetElementEfficacy(_elementType).EfficacyRate / 100;

        return Mathf.Round((userPower - targetDefence) * efficacyModifier);
    }
}
