using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TranslatedText))]
public class TranslatedTextDrawer : PropertyDrawer
{
    private bool _displayValue;
    private float _propertyHeight;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (_displayValue)
        {
            return _propertyHeight + 25;
        }

        return 20;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        position.width -= 40;
        position.height = 18;

        Rect foldoutRect = new Rect(position);
        foldoutRect.x -= 10;
        foldoutRect.width = 10;

        _displayValue = EditorGUI.Foldout(foldoutRect, _displayValue, "");

        SerializedProperty key = property.FindPropertyRelative("_key");
        key.stringValue = EditorGUI.TextField(position, key.stringValue);

        GUIContent editContent = new GUIContent("+", "Вызвать окно добавления/редактирования");
        Rect editRect = new Rect(position);
        editRect.width = 18;
        editRect.x += position.width + 2.5f;

        if (GUI.Button(editRect, editContent))
        {
            TranslationEditWindow.Open(key.stringValue);
        }

        GUIContent searchContent = new GUIContent("F", "Вызвать окно поиска.");
        Rect searchRect = new Rect(editRect);
        searchRect.x += editRect.width + 2.5f;

        if (GUI.Button(searchRect, searchContent))
        {
            TranslationSearchWindow.Open();
        }

        Rect valueRect = new Rect(position);

        if (_displayValue)
        {
            string value = Translator.GetTranslationLine(key.stringValue);
            GUIStyle style = GUI.skin.box;
            _propertyHeight = style.CalcHeight(new GUIContent(value), valueRect.width);

            valueRect.height = _propertyHeight;
            valueRect.y += 21;

            EditorGUI.LabelField(valueRect, value, EditorStyles.wordWrappedLabel);
        }

        EditorGUI.EndProperty();
    }
}
