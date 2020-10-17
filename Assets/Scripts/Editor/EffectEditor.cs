using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Effect), true)]
public class EffectEditor : Editor
{
    private Effect _effect = null;
    private bool _editTooltip = false;

    private void OnEnable()
    {
        TranslationSystem.Initialize();

        _effect = (Effect)target;
    }

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();

        EditorGUILayout.BeginVertical("box");

        _editTooltip = EditorGUILayout.ToggleLeft("Редактировать всплывающую подсказку", _editTooltip);

        if (_editTooltip)
        {
            GUIStyle labelStyle = new GUIStyle();
            labelStyle.wordWrap = true;
            labelStyle.normal.textColor = Color.grey;

            string labelText = TranslationSystem.GetValue(_effect.TooltipKey, Translator.CurrentLanguage);

            EditorGUILayout.LabelField(labelText, labelStyle);

            if (GUILayout.Button("Сгенерировать всплывающую подсказку."))
            {
                BakeEffectTooltip(SystemLanguage.Russian, SystemLanguage.English);
            }

            SerializedProperty tooltipTextProperty = serializedObject.FindProperty("_tooltipText");
            EditorGUILayout.PropertyField(tooltipTextProperty);
        }

        EditorGUILayout.EndVertical();
    }

    private void BakeEffectTooltip(params SystemLanguage[] languages)
    {
        foreach (var language in languages)
        {
            Translator.SetTranslation(language);
            TranslationSystem.AddLine(_effect.TooltipKey,
                                      _effect.BuildTooltipText(),
                                      language);
            TranslationSystem.SaveChanges(language, string.Empty);
        }
    }
}
