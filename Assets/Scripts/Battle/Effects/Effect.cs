using UnityEngine;

public abstract class Effect : ScriptableObject
{
    [Header("Общие параметры эффекта:")]
    [SerializeField] protected int _duration = 0;
    [SerializeField] protected int _secondsAmountOverTick = 1;
    [SerializeField] protected Sprite _effectIcon = null;
    [SerializeField, Range(0, 100)] protected int _inflictionChance = 0;
    [SerializeField] protected int _stacksAmount = 1;
    [SerializeField] protected int _order = 0;

    public int StacksAmount => _stacksAmount;
    public int Duration => _duration;
    public int SecondsAmountOverTick => _secondsAmountOverTick;
    public Sprite EffectIcon => _effectIcon;
    public int Order => _order;

    public abstract void Apply(Character target, object effectSource);

    public abstract void Tick(Character target);

    public abstract void Remove(Character target, object effectSource);

    public abstract string GetDescription(string targetType);
}
