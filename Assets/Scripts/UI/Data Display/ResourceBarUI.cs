using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Полоса ресурса на UI.
/// </summary>
public class ResourceBarUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _valueTextField = null;
    [SerializeField] private Image _valueMaskFiller = null;
    [SerializeField] private Slider _valueSlider = null;

    private CharacterResource _resource; // Максимальное значение ресурса.

    public void RegisterResource(CharacterResource resource)
    {
        _resource = resource;
        UpdateBar(resource.CurrentValue);
    }

    public void UpdateBar(float currentValue)
    {
        float newValue = currentValue / _resource.MaxValue.GetFinalValue();

        //_valueMaskFiller.rectTransform.localScale = new Vector2(scale, 1f);
        _valueSlider.value = newValue;

        _valueTextField.SetText($"{currentValue} / {_resource.MaxValue.GetFinalValue()}");
    }
}
