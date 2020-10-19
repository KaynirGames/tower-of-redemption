using UnityEngine;

/// <summary>
/// Текстовые данные об умении.
/// </summary>
[CreateAssetMenu(fileName = "SkillType Skill Data", menuName = "Scriptable Objects/Text Data/Skill Data")]
public class SkillData : ScriptableObject
{
    [SerializeField] private TranslatedText _typeName = new TranslatedText("SkillType.TypeName");
    [SerializeField] private TranslatedText _descriptionFormat = new TranslatedText("SkillType.Format");

    public string TypeName => _typeName.Value;
    public string DescriptionFormat => _descriptionFormat.Value;
}
