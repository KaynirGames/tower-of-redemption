using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Полоса ресурса на UI.
/// </summary>
public class ResourceBarUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _valueTextField = null;
    [SerializeField] private Slider _valueSlider = null;

    private CharacterResource _resource;

    public void RegisterResource(CharacterResource resource)
    {
        _resource = resource;
        UpdateBar(resource.CurrentValue);
    }

    public void UpdateBar(float currentValue)
    {
        float newValue = currentValue / _resource.MaxValue.GetFinalValue();

        _valueSlider.value = newValue;

        _valueTextField.SetText($"{currentValue} / {_resource.MaxValue.GetFinalValue()}");
    }

    public void Clear()
    {
        _resource = null;
    }
}
