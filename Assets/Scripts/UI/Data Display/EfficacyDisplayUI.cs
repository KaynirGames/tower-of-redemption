using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EfficacyDisplayUI : MonoBehaviour
{
    [Header("Текстовые поля с эффективностью элементов:")]
    [SerializeField] private TextMeshProUGUI _fireEfficacyField = null;
    [SerializeField] private TextMeshProUGUI _earthEfficacyField = null;
    [SerializeField] private TextMeshProUGUI _airEfficacyField = null;
    [SerializeField] private TextMeshProUGUI _waterEfficacyField = null;

    private Dictionary<ElementType, TextMeshProUGUI> _efficacyTextFields;

    private CharacterStats _stats;

    public void RegisterElementEfficacies(CharacterStats stats)
    {
        _stats = stats;

        _efficacyTextFields = CreateEfficacyTextFieldDictionary();

        DisplayElementEfficacies();
    }

    private void UpdateEfficacyDisplay(ElementType elementType)
    {
        if (_efficacyTextFields.ContainsKey(elementType))
        {
            _efficacyTextFields[elementType].SetText(_stats.GetElementEfficacy(elementType)
                                                           .ToString());
        }
    }

    private Dictionary<ElementType, TextMeshProUGUI> CreateEfficacyTextFieldDictionary()
    {
        return new Dictionary<ElementType, TextMeshProUGUI>()
        {
            { ElementType.Fire, _fireEfficacyField },
            { ElementType.Earth, _earthEfficacyField },
            { ElementType.Air, _airEfficacyField },
            { ElementType.Water, _waterEfficacyField }
        };
    }

    private void DisplayElementEfficacies()
    {
        UpdateEfficacyDisplay(ElementType.Fire);
        UpdateEfficacyDisplay(ElementType.Earth);
        UpdateEfficacyDisplay(ElementType.Air);
        UpdateEfficacyDisplay(ElementType.Water);
    }
}
