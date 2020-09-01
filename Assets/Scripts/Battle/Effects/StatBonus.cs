using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Amount_Name_SB", menuName = "Scriptable Objects/Battle/Effects/Stat Bonus")]
public class StatBonus : Effect
{
    [SerializeField] private float _bonusValue = 0f;
    [SerializeField] private StatType _statType = StatType.Strength;
    [SerializeField] private TranslatedText _statNameText = null;

    public override void Apply(CharacterStats target)
    {
        StatModifier statModifier = new StatModifier(_bonusValue, this);
        target.GetStat(_statType).AddModifier(statModifier);
        target.UpdateStatDisplay(_statType);

        target.StatBonuses.Add(this);
    }

    public override void Remove(CharacterStats target)
    {
        target.GetStat(_statType).RemoveSourceModifiers(this);
        target.UpdateStatDisplay(_statType);

        target.StatBonuses.Remove(this);
    }

    public override void BuildDescription(StringBuilder stringBuilder)
    {
        if (_bonusValue > 0) stringBuilder.Append("+");
        stringBuilder.Append(_bonusValue);
        stringBuilder.Append(" ");
        stringBuilder.Append(_statNameText.Value);
    }
}