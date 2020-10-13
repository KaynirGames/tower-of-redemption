using UnityEngine;

[CreateAssetMenu(fileName = "STAT+# Bonus", menuName = "Scriptable Objects/Battle/Effects/Stat Bonus")]
public class StatBonus : Effect
{
    [Header("Параметры бонуса к статам:")]
    [SerializeField] private int _bonusValue = 0;
    [SerializeField] private StatData _statData = null;

    public override void Apply(Character target, object effectSource)
    {
        EffectInstance newInstance = new EffectInstance(this, target, effectSource);
        StatModifier modifier = new StatModifier(_bonusValue, newInstance);

        target.Stats.AddStatModifier(_statData.StatType, modifier);
        target.Effects.StatBonuses.Add(newInstance);
    }

    public override void Tick(Character target) { }

    public override void Remove(Character target, object effectSource)
    {
        EffectInstance current = target.Effects.StatBonuses.Find(x => x.EffectSource == effectSource);

        target.Stats.RemoveStatModifier(_statData.StatType, current);
        target.Effects.StatBonuses.Remove(current);
    }

    public override string GetDescription(string targetType)
    {
        return string.Format("{0:+0;-#} {1}",
                             _bonusValue,
                             _statData.StatName);
    }
}
