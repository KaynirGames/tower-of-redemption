using UnityEngine;

/// <summary>
/// Физический урон.
/// </summary>
[CreateAssetMenu(fileName = "NewPhysDamage", menuName = "Scriptable Objects/Battle/Damage/Types/Physical Damage")]
public class PhysicalDamage : Damage
{
    public override float CalculateDamage(Character owner, Character opponent, DamageTier tier)
    {
        float ownerAttack = owner.Stats.GetStat(StatType.Strength)
                                       .GetFinalValue() * tier.AttackPower;

        float opponentDefenceRate = 1 - (opponent.Stats.GetStat(StatType.Defence)
                                                       .GetFinalValue() / 100);

        return Mathf.Round(ownerAttack * opponentDefenceRate);
    }

    public override float CalculateDamage(Character target, float baseDamage)
    {
        float opponentDefenceRate = 1 - (target.Stats.GetStat(StatType.Defence)
                                                       .GetFinalValue() / 100);

        return Mathf.Round(baseDamage * opponentDefenceRate);
    }
}
