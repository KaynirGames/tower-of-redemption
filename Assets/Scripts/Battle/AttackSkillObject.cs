using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Default AttackSkill", menuName = "Scriptable Objects/Battle/Skill Objects/Attack Skill Object")]
public class AttackSkillObject : SkillObject
{
    [Header("Параметры атакующего умения:")]
    [SerializeField] private PowerTier _powerTier = null;
    [SerializeField] private DamageType[] _damageTypes = null;

    public override void Execute(Character owner, Character opponent)
    {
        float finalDamage = 0;

        foreach (DamageType damage in _damageTypes)
        {
            finalDamage += damage.CalculateDamage(owner.Stats,
                                                  opponent.Stats,
                                                  _powerTier.AttackPower);
        }

        owner.Stats.ChangeEnergy(-_cost);
        opponent.Stats.ChangeHealth(-finalDamage);
    }

    public override string GetDescription()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("(")
               .Append(_powerTier.TierName)
               .Append(") ")
               .Append(FlavorText)
               .AppendLine();

        //foreach (DamageType damage in _damageTypes)
        //{

        //}

        return builder.ToString();
    }
}
