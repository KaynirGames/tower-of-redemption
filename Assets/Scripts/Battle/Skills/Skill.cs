using System.Text;
using UnityEditor;
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
    [SerializeField] protected BookSlotType _slotType = BookSlotType.Active;

    public string SkillName => _skillNameText.Value;
    public SkillType SkillType => _skillType;
    public string Description => _descriptionText.Value;
    public Sprite Icon => _icon;

    public float Cost => _cost;
    public float Cooldown => _cooldown;
    public BookSlotType SlotType => _slotType;

    public string ID { get; private set; }

    public abstract void Activate(Character owner, Character opponent);

    public abstract void Deactivate(Character owner, Character opponent);

    public abstract void BuildParamsDescription(StringBuilder builder);

    protected void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        ID = AssetDatabase.AssetPathToGUID(path);
    }
}