using UnityEngine;

/// <summary>
/// Тип умения.
/// </summary>
[CreateAssetMenu(fileName = "Undefined SkillType", menuName = "Scriptable Objects/Battle/Skills/Skill Type")]
public class SkillType : ScriptableObject
{
    [Header("Локализация типа умения:")]
    [SerializeField] private TranslatedText _typeName = new TranslatedText("SkillType.TypeName");
    [SerializeField] private TranslatedText _descriptionFormat = new TranslatedText("SkillType.Format");
    [Header("Локализация типа цели умения:")]
    [SerializeField] private TranslatedText _targetOwner = new TranslatedText("TargetType.Owner");
    [SerializeField] private TranslatedText _targetOpponent = new TranslatedText("TargetType.Opponent");

    public string TypeName => _typeName.Value;
    public string DescriptionFormat => _descriptionFormat.Value;

    public string TargetOwner => _targetOwner.Value;
    public string TargetOpponent => _targetOpponent.Value;
}
