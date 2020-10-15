using UnityEngine;

public abstract class Effect : ScriptableObject
{
    [Header("Общие параметры эффекта:")]
    [SerializeField] protected int _duration = 0;
    [SerializeField] protected int _secondsAmountOverTick = 1;
    [SerializeField, Range(0, 100)] protected int _inflictionChance = 0;
    [SerializeField] protected int _displayOrder = 0;

    public int Duration => _duration;
    public int SecondsAmountOverTick => _secondsAmountOverTick;
    public int DisplayOrder => _displayOrder;

    public virtual int StacksAmount => 0;
    public virtual Sprite EffectIcon => null;

    public abstract void Apply(Character target, object effectSource);

    public abstract void Tick(Character target);

    public abstract void Remove(Character target, object effectSource);

    public abstract string GetDescription(string targetType);

    protected bool ThrowInflictionChanceDice()
    {
        return Random.Range(0, 100) < _inflictionChance
            ? true
            : false;
    }
}