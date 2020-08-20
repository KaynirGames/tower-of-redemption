using TMPro;
using UnityEngine;

public class TranslatedTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textField = null; // Текстовое поле для отображения.
    [SerializeField] private TranslatedText _translatedText = null; // Переведенный текст.

    public void Start()
    {
        _textField.SetText(_translatedText.Value);
    }
}
