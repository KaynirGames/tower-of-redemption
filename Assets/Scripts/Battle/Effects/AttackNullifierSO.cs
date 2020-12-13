using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nullify Type Charges_Duration", menuName = "Scriptable Objects/Battle/Effects/Attack Nullifier SO")]
public class AttackNullifierSO : EffectSO
{
    [Header("Параметры обнуления атаки:")]
    [SerializeField] private int _chargesAmount = 1;
    [SerializeField] private AilmentSO _ailmentData = null;
    [SerializeField] private List<DamageSO> _nullableDamageTypes = null;

    public override int ChargesAmount => _chargesAmount;
    public override Sprite EffectIcon => _ailmentData.Icon;

    public override void Apply(Character target, object effectSource)
    {
        if (!TryRestartEffect(target))
        {
            Effect effectInstance = new Effect(this, target, effectSource);

            target.Effects.AilmentEffects.Add(_ailmentData, effectInstance);

            effectInstance.StartDuration();
        }
    }

    public override void Remove(Character target, object effectSource)
    {
        target.Effects.AilmentEffects.Remove(_ailmentData);
    }

    public override void Tick(Character target) { }

    public bool IsAttackNullable(DamageSO damageType)
    {
        return _nullableDamageTypes.Contains(damageType);
    }

    protected override bool TryRestartEffect(Character target)
    {
        return _ailmentData.TryRestartAilment(target, this);
    }
}
