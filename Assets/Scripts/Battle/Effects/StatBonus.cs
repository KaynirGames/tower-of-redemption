using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Amount_Stat_SB", menuName = "Scriptable Objects/Battle/Effects/Stat Bonus")]
public class StatBonus : Effect
{
    [SerializeField] private StatModifier _modifier = null;
    [SerializeField] private StatType _statType = StatType.Strength;
    [SerializeField] private TranslatedText _statNameText = null;

    public override void Apply(Character target)
    {
        target.Stats.GetStat(_statType).AddModifier(_modifier);
        target.Stats.UpdateStatDisplay(_statType);

        target.Stats.StatBonuses.Add(this);
    }

    public override void Remove(Character target)
    {
        target.Stats.GetStat(_statType).RemoveModifier(_modifier);
        target.Stats.UpdateStatDisplay(_statType);

        target.Stats.StatBonuses.Remove(this);
    }

    public override void BuildDescription(StringBuilder builder)
    {
        if (_modifier.Value > 0) { builder.Append("+"); }

        builder.Append(_modifier.Value);
        builder.Append(" ");
        builder.Append(_statNameText.Value);
    }
}