using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// Пассивное умение.
/// </summary>
[CreateAssetMenu(fileName = "Name_PS", menuName = "Scriptable Objects/Battle/Skills/Passive Skill")]
public class PassiveSkill : Skill
{
    [Header("Параметры пассивного умения:")]
    [SerializeField] private List<StatBonus> _statBonuses = new List<StatBonus>();

    public override void Activate(Character owner, Character opponent)
    {
        foreach (StatBonus bonus in _statBonuses)
        {
            bonus.Apply(owner);
        }
    }

    public override void Deactivate(Character owner, Character opponent)
    {
        foreach (StatBonus bonus in _statBonuses)
        {
            bonus.Remove(owner);
        }
    }

    public override void BuildParamsDescription(StringBuilder builder)
    {
        _statBonuses.ForEach(bonus => bonus.BuildDescription(builder));
    }
}
