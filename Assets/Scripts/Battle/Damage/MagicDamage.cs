using UnityEngine;

/// <summary>
/// Магический урон.
/// </summary>
[CreateAssetMenu(fileName = "NewMagicDamage", menuName = "Scriptable Objects/Battle/Damage/Types/Magic Damage")]
public class MagicDamage : Damage
{
    [SerializeField] private ElementType _elementType = ElementType.Fire;

    public override float CalculateDamage(Character owner, Character opponent, DamageTier tier)
    {
        float ownerAttack = owner.Stats.GetStat(StatType.Will)
                                       .GetFinalValue() * tier.AttackPower;

        float opponentDefenceRate = 1 - (opponent.Stats.GetStat(StatType.MagicDefence)
                                                       .GetFinalValue() / 100);

        float efficacyModifier = opponent.Stats.GetElementEfficacy(_elementType) / 100;

        return Mathf.Round(ownerAttack * opponentDefenceRate * efficacyModifier);
    }

    public override float CalculateDamage(Character target, float baseDamage)
    {
        float opponentDefenceRate = 1 - (target.Stats.GetStat(StatType.MagicDefence)
                                                       .GetFinalValue() / 100);

        float efficacyModifier = target.Stats.GetElementEfficacy(_elementType) / 100;

        return Mathf.Round(baseDamage * opponentDefenceRate * efficacyModifier);
    }
}
