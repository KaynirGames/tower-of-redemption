using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackSkill", menuName = "Scriptable Objects/Battle/Skills/Attack Skill")]
public class AttackSkill : Skill
{
    [Header("Параметры атакующего умения:")]
    [SerializeField] private PowerTier _powerTier = null;
    [SerializeField] private DamageType[] _damageTypes = null;

    public override void Activate(Character owner, Character opponent)
    {
        float finalDamage = 0;

        foreach (DamageType damageType in _damageTypes)
        {
            finalDamage += damageType.CalculateDamage(owner.Stats, opponent.Stats, _powerTier);
        }

        opponent.Stats.ChangeHealth(finalDamage);
        owner.Stats.ChangeEnergy(-_cost);
    }

    public override void Deactivate(Character owner, Character opponent) { }

    public override void BuildParamsDescription(StringBuilder stringBuilder)
    {
        stringBuilder.Append(GameTexts.Instance.DamageLabel);
        stringBuilder.Append(": ");

        stringBuilder.Append(_damageTypes[0].Name);

        for (int i = 1; i < _damageTypes.Length; i++)
        {
            stringBuilder.Append(" / ");
            stringBuilder.Append(_damageTypes[i].Name);
        }

        stringBuilder.Append(" (");
        stringBuilder.Append(GameTexts.Instance.PowerTierLabel);
        stringBuilder.Append(": ");
        stringBuilder.Append(_powerTier.TierName);
        stringBuilder.AppendLine(")");
    }
}
