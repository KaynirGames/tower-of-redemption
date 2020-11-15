using UnityEngine;

public abstract class EffectSO : ScriptableObject
{
    [Header("Общие параметры эффекта:")]
    [SerializeField] protected int _duration = 0;
    [SerializeField] protected int _secondsAmountOverTick = 1;
    [SerializeField, Range(0, 100)] protected int _inflictionChance = 100;

    public int Duration => _duration;
    public int SecondsAmountOverTick => _secondsAmountOverTick;

    public virtual int ChargesAmount => 0;
    public virtual Sprite EffectIcon => null;

    public abstract void Apply(Character target, object effectSource);

    public abstract void Tick(Character target);

    public abstract void Remove(Character target, object effectSource);

    protected virtual bool TryRestartEffect(Character target)
    {
        return false;
    }

    protected bool ThrowInflictionChanceDice()
    {
        return Random.Range(1, 100) <= _inflictionChance
            ? true
            : false;
    }
}