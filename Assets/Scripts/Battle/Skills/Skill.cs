using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// Умение персонажа.
/// </summary>
public abstract class Skill : ScriptableObject
{
    [Header("Информация для отображения:")]
    [SerializeField] protected TranslatedText _skillNameText = null;
    [SerializeField] protected TranslatedText _descriptionText = null;
    [SerializeField] protected SkillType _skillType = null;
    [SerializeField] protected Sprite _icon = null;
    [Header("Общие параметры умения:")]
    [SerializeField] protected float _cost = 0;
    [SerializeField] protected float _cooldown = 0;
    [SerializeField] protected TargetType _targetType = TargetType.Opponent;
    [SerializeField] protected SkillSlotType _skillSlotType = SkillSlotType.ActiveSlot;
    [SerializeField] protected List<Effect> _ownerEffects = new List<Effect>();
    [SerializeField] protected List<Effect> _opponentEffects = new List<Effect>();

    public string SkillName => _skillNameText.Value;
    public SkillType SkillType => _skillType;
    public string Description => _descriptionText.Value;
    public Sprite Icon => _icon;

    public float Cost => _cost;
    public float Cooldown => _cooldown;

    public TargetType TargetType => _targetType;
    public SkillSlotType SkillSlotType => _skillSlotType;

    public virtual void BuildParamsDescription(StringBuilder stringBuilder) { }
}