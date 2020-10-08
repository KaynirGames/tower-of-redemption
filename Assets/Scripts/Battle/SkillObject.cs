using UnityEngine;

public abstract class SkillObject : ScriptableObject
{
    [Header("Параметры отображения умения:")]
    [SerializeField] protected TranslatedText _name = null;
    [SerializeField] protected TranslatedText _flavorText = null;
    [SerializeField] protected TranslatedText _skillType = new TranslatedText("SkillType.Attack");
    [SerializeField] protected Sprite _icon = null;
    [SerializeField] protected SkillSlot _slot = SkillSlot.Active;
    [Header("Общие параметры умения:")]
    [SerializeField] protected float _cost = 0;
    [SerializeField] protected float _cooldown = 0;

    public string Name => _name.Value;
    public string FlavorText => _flavorText.Value;
    public string SkillType => _skillType.Value;
    public Sprite Icon => _icon;
    public SkillSlot Slot => _slot;

    public float Cost => _cost;
    public float Cooldown => _cooldown;

    public abstract void Execute(Character owner, Character opponent);

    public abstract string GetDescription();
}
