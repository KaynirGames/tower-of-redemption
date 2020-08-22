using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Полоса ресурса на UI.
/// </summary>
public class ResourceBarUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _valueTextField = null; // Текстовое поле со значением ресурса.
    [SerializeField] private Image _valueMaskFiller = null; // Для отображения значения ресурса.
    [SerializeField] private bool _isVertical = false; // Вид отображения полосы ресурса.

    private Stat _maxValue; // Максимальное значение ресурса.
    /// <summary>
    /// Инициализировать полосу ресурса.
    /// </summary>
    public void Init(Stat maxValue, float currentValue)
    {
        _maxValue = maxValue;
        UpdateBar(currentValue);
    }
    /// <summary>
    /// Обновить полосу ресурса.
    /// </summary>
    public void UpdateBar(float currentValue)
    {
        float scale = currentValue / _maxValue.GetValue();

        _valueMaskFiller.rectTransform.localScale = _isVertical
            ? new Vector2(1f, scale)
            : new Vector2(scale, 1f);

        _valueTextField.SetText($"{currentValue} / {_maxValue.GetValue()}");
    }
}
