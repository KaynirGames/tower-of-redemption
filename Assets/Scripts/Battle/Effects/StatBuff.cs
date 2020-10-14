using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat BuffGrade # sec", menuName = "Scriptable Objects/Battle/Effects/Stat Buff")]
public class StatBuff : Effect
{
    [Header("Параметры бафа стата:")]
    [SerializeField] private StatBuffGrade _buffGrade = null;
    [SerializeField] private StatData _statData = null;

    public StatType StatType => _statData.StatType;
    public StatBuffGrade BuffGrade => _buffGrade;

    public override void Apply(Character target, object effectSource)
    {
        var statBuffs = target.Effects.GetStatBuffs(_buffGrade.IsPositive);
        bool isBuffRestarted = false;

        if (statBuffs.ContainsKey(StatType))
        {
            isBuffRestarted = TryRestartBuff(this, statBuffs);
        }

        if (!isBuffRestarted)
        {
            EffectInstance newInstance = new EffectInstance(this, target, effectSource);

            Stat stat = target.Stats.GetStat(StatType);
            float buffValue = _buffGrade.CalculateBuffValue(stat.BaseValue);

            StatModifier statModifier = new StatModifier(buffValue, newInstance);
            target.Stats.AddStatModifier(StatType, statModifier);

            statBuffs.Add(StatType, newInstance);

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

    public override string GetDescription(string targetType)
    {
        return _buffGrade.GetDescription(_statData.StatName,
                                         targetType,
                                         _duration);
    }

    private bool TryRestartBuff(StatBuff buff, Dictionary<StatType, EffectInstance> statBuffs)
    {
        EffectInstance currentInstance = statBuffs[buff.StatType];
        StatBuff currentBuff = (StatBuff)currentInstance.Effect;

        if (buff.BuffGrade.Priority > currentBuff.BuffGrade.Priority)
        {
            currentInstance.RemoveEffect();
            statBuffs.Remove(currentBuff.StatType);
            return false;
        }
        else
        {
            currentInstance.ResetDuration();
            return true;
        }
    }
}
