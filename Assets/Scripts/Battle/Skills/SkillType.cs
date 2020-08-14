using UnityEngine;

/// <summary>
/// Тип умения.
/// </summary>
[CreateAssetMenu(fileName = "TypeSkill", menuName = "Scriptable Objects/Battle/Skill Type")]
public class SkillType : ScriptableObject
{
    [SerializeField] private TranslatedText _typeNameKey = null;
    /// <summary>
    /// Наименование типа умения.
    /// </summary>
    public string TypeName => _typeNameKey.Value;
}
