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

    private CharacterResource _resource; // Максимальное значение ресурса.

    public void RegisterResource(CharacterResource resource)
    {
        _resource = resource;
        UpdateBar(resource.CurrentValue);
    }

    public void UpdateBar(float currentValue)
    {
        float scale = currentValue / _resource.MaxValue.GetFinalValue();

        _valueMaskFiller.rectTransform.localScale = _isVertical
            ? new Vector2(1f, scale)
            : new Vector2(scale, 1f);

        _valueTextField.SetText($"{currentValue} / {_resource.MaxValue.GetFinalValue()}");
    }
}
