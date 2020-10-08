using UnityEngine;

/// <summary>
/// Физический урон.
/// </summary>
[CreateAssetMenu(fileName = "NewPhysDamage", menuName = "Scriptable Objects/Battle/Damage Types/Physical Damage")]
public class PhysicalDamage : DamageType
{
    public override float CalculateDamage(CharacterStats ownerStats, CharacterStats opponentStats, float attackPower)
    {
        float ownerAttack = ownerStats.GetStat(StatType.Strength)
                                      .GetFinalValue() * attackPower;

        float opponentDefenceRate = 1 - (opponentStats.GetStat(StatType.Defence)
                                                      .GetFinalValue() / 100);

        return Mathf.Round(ownerAttack * opponentDefenceRate);
    }
}
