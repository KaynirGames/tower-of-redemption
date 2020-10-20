using UnityEngine;

[CreateAssetMenu(fileName = "Stat BuffGrade Sec#", menuName = "Scriptable Objects/Battle/Effects/Stat Buff")]
public class StatBuff : Effect
{
    [Header("Параметры бафа стата:")]
    [SerializeField] private StatBuffGrade _buffGrade = null;
    [SerializeField] private StatData _statData = null;
    [SerializeField] private TextColorData _textColorData = null;
    [SerializeField] private Sprite _buffIcon = null;

    public StatType StatType => _statData.StatType;
    public StatBuffGrade BuffGrade => _buffGrade;

    public override Sprite EffectIcon => _buffIcon;

    public override void Apply(Character target, object effectSource)
    {
        if (!TryRestartEffect(target))
        {
            EffectInstance newInstance = new EffectInstance(this, target, effectSource);

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

    public override string GetDescription()
    {
        return string.Format(_descriptionFormat.Value,
                             _textColorData.HtmlTextColor,
                             _statData.StatShrinkName,
                             _duration);
    }

    protected override bool TryRestartEffect(Character target)
    {
        var statBuffs = target.Effects.GetStatBuffs(_buffGrade.IsPositive);

        if (statBuffs.ContainsKey(StatType))
        {
            EffectInstance currentInstance = statBuffs[StatType];
            StatBuff currentBuff = (StatBuff)currentInstance.Effect;

            if (_buffGrade.Priority < currentBuff.BuffGrade.Priority)
            {
                currentInstance.ResetDuration();
                return true;
            }
            else
            {
                currentInstance.RemoveEffect();
                return false;
            }
        }

        return false;
    }
}
