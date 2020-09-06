using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "STAT Amount", menuName = "Scriptable Objects/Battle/Effects/Stat Bonus")]
public class StatBonus : Effect
{
    [SerializeField] private StatModifier _modifier = null;
    [SerializeField] private StatType _statType = StatType.Strength;
    [SerializeField] private TranslatedText _statNameText = null;

    public override void Apply(Character target)
    {
        target.Stats.AddStatModifier(_statType, _modifier);
        target.Effects.AddStatBonus(this);
    }

    public override void Remove(Character target)
    {
        target.Stats.RemoveStatModifier(_statType, _modifier);
        target.Effects.RemoveStatBonus(this);
    }

    public override void BuildDescription(StringBuilder builder)
    {
        builder.Append(_statNameText.Value);

        if (_modifier.Value > 0) { builder.Append("+"); }

        builder.Append(_modifier.Value);
        builder.AppendLine();
    }

    public override string GetDescription(TargetType targetType)
    {
        throw new System.NotImplementedException();
    }
}