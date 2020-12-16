using UnityEngine;

[CreateAssetMenu(fileName = "DoT Rank# Chance#", menuName = "Scriptable Objects/Battle/Effects/Damage Over Time SO")]
public class DamageOverTimeSO : EffectSO
{
    [Header("Параметры ДоТ эффекта:")]
    [SerializeField] private float _damageOverTick = 0f;
    [SerializeField] private DamageSO _damageType = null;
    [SerializeField, Range(-100, 100)] private int _baseStatModifyRate = 0;
    [SerializeField] private StatSO _statData = null;
    [SerializeField] private AilmentSO _ailmentData = null;

    public AilmentSO AilmentData => _ailmentData;
    public override Sprite EffectIcon => _ailmentData.Icon;

    public override void Apply(Character target, object effectSource)
    {
        if (!ThrowInflictionChanceDice()) { return; }

        if (!TryRestartEffect(target))
        {
            Effect effectInstance = new Effect(this, target, effectSource);

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

    protected override bool TryRestartEffect(Character target)
    {
        return _ailmentData.TryRestartAilment(target, this);
    }
}
