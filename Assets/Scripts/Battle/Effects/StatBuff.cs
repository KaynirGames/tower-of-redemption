using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat Up/Down Tier X sec", menuName = "Scriptable Objects/Battle/Effects/Stat Buff")]
public class StatBuff : Effect, ITickable
{
    [SerializeField] private PowerTier _powerTier = null;
    [SerializeField] private StatBuffType _statBuffType = null;
    [SerializeField] private float _duration = 0f;

    public StatBuffType StatBuffType => _statBuffType;

    private Character _target;
    private StatModifier _statModifier;
    private float _timer;

    public override void Apply(Character target)
    {
        _target = target;
        _timer = _duration;

        _statModifier = _statBuffType.CreateStatModifier(_powerTier.StatBuffPower,
                                                         target.Stats);

        target.Stats.AddStatModifier(_statBuffType.StatType, _statModifier);
        target.Effects.AddBuffEffect(this);
    }

    public override void Remove(Character target)
    {
        target.Stats.RemoveStatModifier(_statBuffType.StatType, _statModifier);
        target.Effects.RemoveBuffEffect(this);
    }

    public override void BuildDescription(StringBuilder builder)
    {
        
    }

    public override string GetDescription(TargetType targetType)
    {
        return _statBuffType.GetDescription(_powerTier.TierName,
                                            _duration,
                                            targetType);
    }

    public void Tick(float delta)
    {
        _timer -= delta;

        if (_timer <= 0)
        {
            Remove(_target);
        }
    }
}
