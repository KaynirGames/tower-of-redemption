using UnityEditor;
using UnityEngine;

public abstract class Effect : ScriptableObject
{
    [Header("Общие параметры эффекта:")]
    [SerializeField] protected int _duration = 0;
    [SerializeField] protected int _secondsAmountOverTick = 1;
    [SerializeField, Range(0, 100)] protected int _inflictionChance = 0;
    [Header("Параметры описания эффекта:")]
    [SerializeField] protected int _descriptionOrder = 0;
    [SerializeField] protected TranslatedText _descriptionFormat = new TranslatedText("Effect.EffectType.Format");
    [SerializeField] protected TranslatedText _tooltipFormat = null;
    [SerializeField, HideInInspector] protected TranslatedText _tooltipText = null;

    public int Duration => _duration;
    public int SecondsAmountOverTick => _secondsAmountOverTick;
    public int DescriptionOrder => _descriptionOrder;

    public string ID { get; private set; }
    public string TooltipKey { get; private set; }

    public virtual int StacksAmount => 0;
    public virtual Sprite EffectIcon => null;

    public abstract void Apply(Character target, object effectSource);

    public abstract void Tick(Character target);

    public abstract void Remove(Character target, object effectSource);

    public abstract string GetDescription(string targetType);

    public virtual string BuildTooltipText()
    {
        return string.Empty;
    }

    protected bool ThrowInflictionChanceDice()
    {
        return Random.Range(0, 100) < _inflictionChance
            ? true
            : false;
    }

    protected virtual void OnValidate()
    {
        string assetPath = AssetDatabase.GetAssetPath(this);
        ID = AssetDatabase.AssetPathToGUID(assetPath);
        _tooltipText = new TranslatedText($"Tooltip.{ID}");
        TooltipKey = _tooltipText.Key;
    }
}