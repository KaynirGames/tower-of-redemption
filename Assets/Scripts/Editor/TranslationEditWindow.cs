using UnityEditor;
using UnityEngine;

public class TranslationEditWindow : EditorWindow
{
    private string _key;
    private string _valueRU;
    private string _valueEN;
    private string _oldKey;
    private bool _createBackup;
    private string _backupFileSuffix;

    public static void Open(string key)
    {
        TranslationEditWindow editWindow = CreateWindow<TranslationEditWindow>();
        editWindow.titleContent = new GUIContent("Редактор перевода");
        editWindow.minSize = new Vector2(640, 280);
        editWindow.maxSize = editWindow.minSize;
        editWindow.ShowUtility();

        editWindow._key = key;
        editWindow._oldKey = key;

        TranslationSystem.Initialize();
        editWindow._valueRU = TranslationSystem.GetValue(key, SystemLanguage.Russian);
        editWindow._valueEN = TranslationSystem.GetValue(key, SystemLanguage.English);
    }

    private void OnGUI()
    {
        _key = EditorGUILayout.TextField("Ключ перевода:", _key);

        EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

        EditorStyles.textArea.wordWrap = true;

        EditorGUILayout.BeginVertical();

        EditorGUILayout.LabelField("Русский язык:");

        _valueRU = EditorGUILayout.TextArea(_valueRU,
                                            EditorStyles.textArea,
                                            GUILayout.Height(120),
                                            GUILayout.Width(310));

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();

        EditorGUILayout.LabelField("Английский язык:");

        _valueEN = EditorGUILayout.TextArea(_valueEN,
                                            EditorStyles.textArea,
                                            GUILayout.Height(120),
                                            GUILayout.Width(310));
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();

        _createBackup = EditorGUILayout.BeginToggleGroup("Создать резервные копии файлов перевода", _createBackup);

        GUILayout.Label("Суффикс в названии резервных копий:");

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("/translationLANG_", GUILayout.Width(110));
        _backupFileSuffix = EditorGUILayout.TextField(_backupFileSuffix, GUILayout.Width(200));
        GUILayout.Label(".json");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndToggleGroup();

        EditorGUILayout.Space(15);

        if (GUILayout.Button("Сохранить", GUILayout.Height(30)))
        {
            if (_key != _oldKey)
            {
                TranslationSystem.UpdateKey(_oldKey, _key, SystemLanguage.Russian);
                TranslationSystem.UpdateKey(_oldKey, _key, SystemLanguage.English);
                _oldKey = _key;
            }

            TranslationSystem.AddLine(_key, _valueRU, SystemLanguage.Russian);
            TranslationSystem.AddLine(_key, _valueEN, SystemLanguage.English);

            string backupRU = string.Empty;
            string backupEN = string.Empty;

            if (_createBackup)
            {
                backupRU = string.Format("translationRU_{0}", _backupFileSuffix);
                backupEN = string.Format("translationEN_{0}", _backupFileSuffix);
            }

            TranslationSystem.SaveChanges(SystemLanguage.Russian, backupRU);
            TranslationSystem.SaveChanges(SystemLanguage.English, backupEN);
        }
    }
}
