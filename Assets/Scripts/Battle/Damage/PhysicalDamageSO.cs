using UnityEngine;

/// <summary>
/// Физический урон.
/// </summary>
[CreateAssetMenu(fileName = "Phys Damage SO", menuName = "Scriptable Objects/Battle/Damage/Physical Damage SO")]
public class PhysicalDamageSO : DamageSO
{
    public override float CalculateDamage(Character owner, Character opponent, float attackPower)
    {
        float ownerAttack = owner.Stats.GetStat(StatType.Strength)
                                       .GetFinalValue() * attackPower;

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
