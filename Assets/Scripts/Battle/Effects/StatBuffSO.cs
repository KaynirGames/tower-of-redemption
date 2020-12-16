using UnityEngine;

[CreateAssetMenu(fileName = "Stat BuffGrade #", menuName = "Scriptable Objects/Battle/Effects/Stat Buff SO")]
public class StatBuffSO : EffectSO
{
    [Header("Параметры бафа стата:")]
    [SerializeField] private StatBuffGradeSO _buffGrade = null;
    [SerializeField] private StatSO _statData = null;
    [SerializeField] private Sprite _buffIcon = null;

    public StatType StatType => _statData.StatType;
    public StatBuffGradeSO BuffGrade => _buffGrade;

    public override Sprite EffectIcon => _buffIcon;

    public override void Apply(Character target, object effectSource)
    {
        if (!TryRestartEffect(target))
        {
            Effect newInstance = new Effect(this, target, effectSource);

            Stat stat = target.Stats.GetStat(StatType);
            float buffValue = _buffGrade.CalculateBuffValue(stat.BaseValue);

            StatModifier statModifier = new StatModifier(buffValue, newInstance);
            target.Stats.AddStatModifier(StatType, statModifier);

            target.Effects.GetStatBuffs(_buffGrade.IsPositive)
                          .Add(StatType, newInstance);

            newInstance.StartDuration();
        }
    }

    public override void Remove(Character target, object effectSource)
    {
        var statBuffs = target.Effects.GetStatBuffs(_buffGrade.IsPositive);
        target.Stats.RemoveStatModifier(StatType, statBuffs[StatType]);

        statBuffs.Remove(StatType);
    }

    public override void Tick(Character target) { }

    protected override bool TryRestartEffect(Character target)
    {
        var statBuffs = target.Effects.GetStatBuffs(_buffGrade.IsPositive);

        if (statBuffs.ContainsKey(StatType))
        {
            Effect currentEffect = statBuffs[StatType];
            StatBuffSO currentBuff = (StatBuffSO)currentEffect.EffectSO;

            if (_buffGrade.Priority < currentBuff.BuffGrade.Priority)
            {
                currentEffect.ResetDuration();
                return true;
            }
            else
            {
                currentEffect.RemoveEffect();
                return false;
            }
        }

        return false;
    }
}
