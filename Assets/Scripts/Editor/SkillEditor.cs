using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Skill), true)]
public class SkillEditor : Editor
{
    private Skill _targetSkill = null;
    private string _descriptionKey = string.Empty;

    private void OnEnable()
    {
        _targetSkill = (Skill)target;

        SerializedProperty description = serializedObject.FindProperty("_description");
        _descriptionKey = description.FindPropertyRelative("_key").stringValue;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical("box");

        GUIStyle labelStyle = new GUIStyle();
        labelStyle.wordWrap = true;
        labelStyle.normal.textColor = Color.grey;

        EditorGUILayout.TextArea(_targetSkill.Description, labelStyle);

        if (GUILayout.Button("Сгенерировать описание умения"))
        {
            BakeSkillDescription(SystemLanguage.Russian, SystemLanguage.English);
        }

        EditorGUILayout.EndVertical();

        base.OnInspectorGUI();
    }

    private void BakeSkillDescription(params SystemLanguage[] languages)
    {
        TranslationSystem.Init();

        foreach (var language in languages)
        {
            Translator.SetTranslation(language);
            TranslationSystem.AddLine(_descriptionKey, _targetSkill.BuildDescription(), language);
            TranslationSystem.SaveChanges(language, string.Empty);
        }
    }
}
