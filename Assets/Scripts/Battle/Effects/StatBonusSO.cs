using UnityEngine;

[CreateAssetMenu(fileName = "STAT+#", menuName = "Scriptable Objects/Battle/Effects/Stat Bonus SO")]
public class StatBonusSO : EffectSO
{
    [Header("Параметры бонуса к статам:")]
    [SerializeField] private int _bonusValue = 0;
    [SerializeField] private StatSO _statData = null;

    public override void Apply(Character target, object effectSource)
    {
        Effect effect = new Effect(this, target, effectSource);
        StatModifier modifier = new StatModifier(_bonusValue, effect);

        target.Stats.AddStatModifier(_statData.StatType, modifier);
        target.Effects.StatBonuses.Add(effect);
    }

    public override void Tick(Character target) { }

    public override void Remove(Character target, object effectSource)
    {
        Effect current = target.Effects.StatBonuses.Find(x => x.EffectSource == effectSource);

        target.Stats.RemoveStatModifier(_statData.StatType, current);
        target.Effects.StatBonuses.Remove(current);
    }
}
