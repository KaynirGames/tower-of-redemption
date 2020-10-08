using UnityEngine;

/// <summary>
/// Магический урон.
/// </summary>
[CreateAssetMenu(fileName = "NewMagicDamage", menuName = "Scriptable Objects/Battle/Damage Types/Magic Damage")]
public class MagicDamage : DamageType
{
    [SerializeField] private ElementType _elementType = ElementType.Fire;

    public override float CalculateDamage(CharacterStats ownerStats, CharacterStats opponentStats, float attackPower)
    {
        float ownerAttack = ownerStats.GetStat(StatType.Will)
                                      .GetFinalValue() * attackPower;

        float opponentDefenceRate = 1 - (opponentStats.GetStat(StatType.MagicDefence)
                                                      .GetFinalValue() / 100);

        float efficacyModifier = opponentStats.GetElementEfficacy(_elementType) / 100;

        return Mathf.Round(ownerAttack * opponentDefenceRate * efficacyModifier);
    }
}
