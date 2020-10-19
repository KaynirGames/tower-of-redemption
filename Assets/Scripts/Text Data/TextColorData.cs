using UnityEngine;

/// <summary>
/// Данные о цвете текста.
/// </summary>
[CreateAssetMenu(fileName = "Undefined TextColorData", menuName = "Scriptable Objects/Text Data/Text Color Data")]
public class TextColorData : ScriptableObject
{
    [SerializeField] private Color _textColor = new Color(0, 0, 0, 255);

    public string HtmlTextColor => ColorUtility.ToHtmlStringRGBA(_textColor);

    public string ColorText(string text)
    {
        return string.Format("<color=#{0}>{1}</color>",
                             ColorUtility.ToHtmlStringRGBA(_textColor),
                             text);
    }
}
