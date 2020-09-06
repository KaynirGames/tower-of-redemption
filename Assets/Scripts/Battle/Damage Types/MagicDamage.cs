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
        float userPower = user.GetStat(StatType.Will).GetFinalValue() * tier.AttackPower;
        float targetDefence = 1 - (target.GetStat(StatType.MagicDefence).GetFinalValue() / 100);
        float efficacyModifier = target.GetElementEfficacy(_elementType) / 100;

        return -Mathf.Round(userPower * targetDefence * efficacyModifier);
    }
}
