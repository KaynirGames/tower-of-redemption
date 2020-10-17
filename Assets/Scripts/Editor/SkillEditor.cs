using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Skill), true)]
public class SkillEditor : Editor
{
    private Skill _targetSkill = null;
    private string _descriptionKey = string.Empty;

    private void OnEnable()
    {
        TranslationSystem.Initialize();

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

        string labelText = TranslationSystem.GetValue(_descriptionKey, Translator.CurrentLanguage);

        EditorGUILayout.LabelField(labelText, labelStyle);

        if (GUILayout.Button("Сгенерировать описание умения."))
        {
            BakeSkillDescription(SystemLanguage.Russian, SystemLanguage.English);
        }

        EditorGUILayout.EndVertical();

        base.OnInspectorGUI();
    }

    private void BakeSkillDescription(params SystemLanguage[] languages)
    {
        if (_descriptionKey == string.Empty) { return; }
        
        foreach (var language in languages)
        {
            Translator.SetTranslation(language);
            TranslationSystem.AddLine(_descriptionKey, _targetSkill.BuildDescription(), language);
            TranslationSystem.SaveChanges(language, string.Empty);
        }
    }
}
