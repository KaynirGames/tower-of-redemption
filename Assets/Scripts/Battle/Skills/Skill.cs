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
    [SerializeField] protected SkillType _skillType = null;
    [SerializeField] protected TranslatedText _descriptionText = null;
    [SerializeField] protected Sprite _icon = null;
    [Header("Общие параметры умения:")]
    [SerializeField] protected float _cost = 0;
    [SerializeField] protected float _cooldown = 0;
    [SerializeField] protected PowerTier _powerTier = null;
    [SerializeField] protected TargetType _targetType = TargetType.Enemy;
    [SerializeField] protected SkillSlotType _skillSlotType = SkillSlotType.ActiveSlot;
    [SerializeField] protected List<SkillEffect> _userEffects = new List<SkillEffect>();
    [SerializeField] protected List<SkillEffect> _enemyEffects = new List<SkillEffect>();

    protected StringBuilder _stringBuilder = new StringBuilder(128, 128); // Для описания.
    /// <summary>
    /// Название умения.
    /// </summary>
    public string SkillName => _skillNameText.Value;
    /// <summary>
    /// Тип умения.
    /// </summary>
    public SkillType SkillType => _skillType;
    /// <summary>
    /// Краткое описание умения.
    /// </summary>
    public string Description => _descriptionText.Value;
    /// <summary>
    /// Иконка умения.
    /// </summary>
    public Sprite Icon => _icon;
    /// <summary>
    /// Стоимость использования умения.
    /// </summary>
    public float Cost => _cost;
    /// <summary>
    /// Время перезарядки умения.
    /// </summary>
    public float Cooldown => _cooldown;
    /// <summary>
    /// Ранг силы умения.
    /// </summary>
    public PowerTier PowerTier => _powerTier;
    /// <summary>
    /// Тип цели умения.
    /// </summary>
    public TargetType TargetType => _targetType;
    /// <summary>
    /// Тип занимаемого слота в книге умений.
    /// </summary>
    public SkillSlotType SkillSlotType => _skillSlotType;
    /// <summary>
    /// Получить описание параметров умения.
    /// </summary>
    public abstract StringBuilder GetParamsDescription();
    /// <summary>
    /// Активировать умение.
    /// </summary>
    public abstract void Activate(CharacterStats user, CharacterStats target);
    /// <summary>
    /// Деактивировать умение.
    /// </summary>
    public abstract void Deactivate(CharacterStats user, CharacterStats target);
}