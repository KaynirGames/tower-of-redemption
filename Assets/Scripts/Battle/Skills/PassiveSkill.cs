using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// Пассивное умение.
/// </summary>
[CreateAssetMenu(fileName = "NewPassiveSkill", menuName = "Scriptable Objects/Battle/Skills/Passive Skill")]
public class PassiveSkill : Skill
{
    [Header("Параметры пассивного умения:")]
    [SerializeField] private List<StatBonus> _statBonuses = new List<StatBonus>();

    public override void Activate(Character owner, Character opponent)
    {
        foreach (StatBonus bonus in _statBonuses)
        {
            //StatBonus newBonus = Instantiate(bonus);
            bonus.Apply(owner);
        }
    }

    public override void Deactivate(Character owner, Character opponent)
    {
        foreach (StatBonus bonus in _statBonuses)
        {
            //StatBonus newBonus = Instantiate(bonus);
            bonus.Remove(owner);
        }
    }

    public override void BuildParamsDescription(StringBuilder stringBuilder)
    {

    }
}
