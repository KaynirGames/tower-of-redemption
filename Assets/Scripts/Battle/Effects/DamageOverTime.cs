using UnityEngine;

[CreateAssetMenu(fileName = "DoTName # dmg #%STAT # sec #%", menuName = "Scriptable Objects/Battle/Effects/Damage Over Time")]
public class DamageOverTime : Effect
{
    [Header("Параметры ДоТ эффекта:")]
    [SerializeField] private float _fullDamageValue = 0f;
    [SerializeField] private Damage _damageType = null;
    [SerializeField, Range(0, 100)] private float _baseStatPenaltyRate = 0f;
    [SerializeField] private StatData _affectedStatData = null;
    [SerializeField] private AilmentData _ailmentData = null;

    public AilmentData AilmentData => _ailmentData;
    public override Sprite EffectIcon => _ailmentData.Icon;

    public override void Apply(Character target, object effectSource)
    {
        if (!ThrowInflictionChanceDice()) { return; }

        if (!_ailmentData.TryRestartAilment(target, this))
        {
            EffectInstance effectInstance = new EffectInstance(this, target, effectSource);

            float statPenalty = target.Stats.GetStat(_affectedStatData.StatType).BaseValue
                                * _baseStatPenaltyRate
                                / 100f;

            StatModifier statModifier = new StatModifier(-statPenalty, effectInstance);
            target.Stats.AddStatModifier(_affectedStatData.StatType, statModifier);

            target.Effects.AilmentEffects.Add(_ailmentData, effectInstance);

            effectInstance.StartDuration();
        }
    }

    public override void Tick(Character target)
    {
        float tickDamage = _damageType.CalculateDamage(target,
                                                       _fullDamageValue / _duration);
        target.Stats.ChangeHealth(-tickDamage);
    }

    public override void Remove(Character target, object effectSource)
    {
        var ailments = target.Effects.AilmentEffects;
        target.Stats.RemoveStatModifier(_affectedStatData.StatType, ailments[_ailmentData]);

        ailments.Remove(_ailmentData);
    }

    public override string GetDescription(string targetType)
    {
        return string.Format(_ailmentData.DisplayFormat,
                             _ailmentData.AilmentName,
                             _inflictionChance);
    }

    private void OnEnable()
    {
        _displayOrder = 2;
    }
}
