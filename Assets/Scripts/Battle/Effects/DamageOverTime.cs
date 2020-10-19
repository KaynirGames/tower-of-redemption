using UnityEngine;

[CreateAssetMenu(fileName = "Name.D#.S#.STAT#.%#", menuName = "Scriptable Objects/Battle/Effects/Damage Over Time")]
public class DamageOverTime : Effect
{
    [Header("Параметры ДоТ эффекта:")]
    [SerializeField] private float _damageOverTick = 0f;
    [SerializeField] private Damage _damageType = null;
    [SerializeField, Range(-100, 100)] private int _baseStatModifyRate = 0;
    [SerializeField] private StatData _statData = null;
    [SerializeField] private TextColorData _statTextColorData = null;
    [SerializeField] private AilmentData _ailmentData = null;

    public AilmentData AilmentData => _ailmentData;
    public override Sprite EffectIcon => _ailmentData.Icon;

    public override void Apply(Character target, object effectSource)
    {
        if (!ThrowInflictionChanceDice()) { return; }

        if (!TryRestartEffect(target))
        {
            EffectInstance effectInstance = new EffectInstance(this, target, effectSource);

            float statModifierValue = target.Stats.GetStat(_statData.StatType).BaseValue
                                      * _baseStatModifyRate
                                      / 100f;

            StatModifier statModifier = new StatModifier(statModifierValue, effectInstance);
            target.Stats.AddStatModifier(_statData.StatType, statModifier);

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
        target.Stats.RemoveStatModifier(_statData.StatType, ailments[_ailmentData]);

        ailments.Remove(_ailmentData);
    }

    public override string GetDescription()
    {
        return string.Format(_descriptionFormat.Value,
                             TooltipKey,
                             _ailmentData.GetAilmentName(),
                             _inflictionChance);
    }

    public override string BuildTooltipText()
    {
        return string.Format(_tooltipFormat.Value,
                             _damageType.GetDamageName(),
                             _damageOverTick,
                             _secondsAmountOverTick,
                             _statTextColorData.HtmlTextColor,
                             _statData.StatName,
                             _baseStatModifyRate,
                             _duration);
    }

    protected override bool TryRestartEffect(Character target)
    {
        var ailments = target.Effects.AilmentEffects;

        if (ailments.ContainsKey(_ailmentData))
        {
            EffectInstance currentInstance = ailments[_ailmentData];

            if (currentInstance.Effect == this)
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

    private void OnEnable()
    {
        _descriptionOrder = 2;
    }
}
