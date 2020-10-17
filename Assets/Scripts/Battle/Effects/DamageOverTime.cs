using UnityEngine;

[CreateAssetMenu(fileName = "Name.D#.S#.STAT#.%#", menuName = "Scriptable Objects/Battle/Effects/Damage Over Time")]
public class DamageOverTime : Effect
{
    [Header("Параметры ДоТ эффекта:")]
    [SerializeField] private float _damageOverTick = 0f;
    [SerializeField] private Damage _damageType = null;
    [SerializeField, Range(-100, 100)] private int _baseStatModifyRate = 0;
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

            float statModifierValue = target.Stats.GetStat(_affectedStatData.StatType).BaseValue
                                * _baseStatModifyRate
                                / 100f;

            StatModifier statModifier = new StatModifier(statModifierValue, effectInstance);
            target.Stats.AddStatModifier(_affectedStatData.StatType, statModifier);

            target.Effects.AilmentEffects.Add(_ailmentData, effectInstance);

            effectInstance.StartDuration();
        }
    }

    public override void Tick(Character target)
    {
        float tickDamage = _damageType.CalculateDamage(target,
                                                       _damageOverTick / _duration);
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
        return string.Format(_descriptionFormat.Value,
                             TooltipKey,
                             _ailmentData.AilmentTextColorHtml,
                             _ailmentData.AilmentName,
                             _inflictionChance);
    }

    public override string BuildTooltipText()
    {
        bool isPositiveStat = _baseStatModifyRate > 0 ? true : false;

        return string.Format(_tooltipFormat.Value,
                             _damageType.GetName(),
                             _damageType.TextColorHtml,
                             _damageOverTick,
                             _secondsAmountOverTick,
                             _affectedStatData.GetRichTextColor(isPositiveStat),
                             _affectedStatData.StatName,
                             _baseStatModifyRate,
                             _duration);
    }

    private void OnEnable()
    {
        _descriptionOrder = 2;
    }
}
