using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Name AttackType Stacks# Sec#", menuName = "Scriptable Objects/Battle/Effects/Attack Nullifier")]
public class AttackNullifier : Effect
{
    [SerializeField] private int _chargesAmount = 1;
    [SerializeField] private AilmentData _ailmentData = null;
    [SerializeField] private List<Damage> _nullableDamageTypes = null;

    public override int ChargesAmount => _chargesAmount;
    public override Sprite EffectIcon => _ailmentData.Icon;

    public override void Apply(Character target, object effectSource)
    {
        if (!TryRestartEffect(target))
        {
            EffectInstance effectInstance = new EffectInstance(this, target, effectSource);

            target.Effects.AilmentEffects.Add(_ailmentData, effectInstance);

            effectInstance.StartDuration();
        }
    }

    public override string GetDescription()
    {
        return string.Format(_descriptionFormat.Value,
                             TooltipKey,
                             _ailmentData.GetAilmentName(),
                             _duration);
    }

    public override void Remove(Character target, object effectSource)
    {
        target.Effects.AilmentEffects.Remove(_ailmentData);
    }

    public override void Tick(Character target) { }

    public override string BuildTooltipText()
    {
        return string.Format(_tooltipFormat.Value,
                             _chargesAmount);
    }

    public bool TryNullifyAttack(Damage damageType)
    {
        return _nullableDamageTypes.Contains(damageType);
    }

    protected override bool TryRestartEffect(Character target)
    {
        return _ailmentData.TryRestartAilment(target, this);
    }
}
