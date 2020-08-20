using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TranslationSearchWindow : EditorWindow
{
    private Dictionary<string, string> _translationRU;
    private Dictionary<string, string> _translationEN;

    private string _searchValue = string.Empty;
    private Vector2 _scroll = Vector2.zero;

    public static void Open()
    {
        TranslationSearchWindow searchWindow = GetWindow<TranslationSearchWindow>();
        searchWindow.titleContent = new GUIContent("Поиск перевода");

        Vector2 mousePos = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
        Rect rect = new Rect(mousePos.x, mousePos.y, 10, 10);

        searchWindow.ShowAsDropDown(rect, new Vector2(720, 480));
    }

    private void OnEnable()
    {
        TranslationSystem.Init();
        _translationRU = TranslationSystem.LoadTranslationData(SystemLanguage.Russian);
        _translationEN = TranslationSystem.LoadTranslationData(SystemLanguage.English);
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal("box");
        EditorGUILayout.LabelField("Поиск:", EditorStyles.boldLabel);
        _searchValue = EditorGUILayout.TextField(_searchValue);
        EditorGUILayout.EndHorizontal();

        DisplaySearchResult();
    }

    private void DisplaySearchResult()
    {
        if (_searchValue == null || _searchValue == string.Empty) { return; }

        EditorGUILayout.BeginVertical();
        _scroll = EditorGUILayout.BeginScrollView(_scroll);

        foreach (var line in _translationEN)
        {
            if (line.Key.ToLower().Contains(_searchValue.ToLower()) ||
                line.Value.ToLower().Contains(_searchValue.ToLower()))
            {
                EditorGUILayout.BeginHorizontal("box");

                if (GUILayout.Button("X", GUILayout.MaxHeight(20), GUILayout.MaxWidth(20)))
                {
                    if (EditorUtility.DisplayDialog($"Удалить ключ {line.Key}?",
                        "Это действие удалит ключ из всех файлов перевода. Вы уверены?",
                        "Конечно.", "Так, стоп..."))
                    {
                        TranslationSystem.RemoveLine(line.Key, SystemLanguage.Russian);
                        TranslationSystem.RemoveLine(line.Key, SystemLanguage.English);
                        TranslationSystem.SaveChanges(SystemLanguage.Russian, string.Empty);
                        TranslationSystem.SaveChanges(SystemLanguage.English, string.Empty);
                        AssetDatabase.Refresh();
                        TranslationSystem.Init();
                        _translationRU = TranslationSystem.LoadTranslationData(SystemLanguage.Russian);
                        _translationEN = TranslationSystem.LoadTranslationData(SystemLanguage.English);
                    }
                }

                EditorGUILayout.TextField(line.Key);

                EditorGUILayout.BeginVertical();

                EditorGUILayout.LabelField(line.Value);

                if (_translationRU.ContainsKey(line.Key))
                {
                    EditorGUILayout.LabelField(_translationRU[line.Key]);
                }

                EditorGUILayout.EndVertical();

                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }
}
