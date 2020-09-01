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
    [SerializeField] private List<StatBonus> _ownerStatBonuses = new List<StatBonus>();
    [SerializeField] private List<StatBonus> _opponentStatBonuses = new List<StatBonus>();

    public void Activate(Character owner, Character opponent)
    {
        if (owner != null)
        {
            _ownerStatBonuses.ForEach(bonus => bonus.Apply(owner.Stats));
        }

        if (opponent != null)
        {
            _opponentStatBonuses.ForEach(bonus => bonus.Apply(opponent.Stats));
        }
    }

    public void Deactivate(Character owner, Character opponent)
    {
        if (owner != null)
        {
            _ownerStatBonuses.ForEach(bonus => bonus.Remove(owner.Stats));
        }

        if (opponent != null)
        {
            _opponentStatBonuses.ForEach(bonus => bonus.Remove(opponent.Stats));
        }
    }

    public override void BuildParamsDescription(StringBuilder stringBuilder)
    {

    }
}
