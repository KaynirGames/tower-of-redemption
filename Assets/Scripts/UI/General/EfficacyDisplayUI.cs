using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EfficacyDisplayUI : MonoBehaviour
{
    [Header("Текстовые поля с эффективностью элементов:")]
    [SerializeField] private TextMeshProUGUI _fireEfficacyField = null;
    [SerializeField] private TextMeshProUGUI _earthEfficacyField = null;
    [SerializeField] private TextMeshProUGUI _waterEfficacyField = null;
    [SerializeField] private TextMeshProUGUI _windEfficacyField = null;

    private Dictionary<ElementType, TextMeshProUGUI> _efficacyTextFields;

    private CharacterStats _stats;

    private void Awake()
    {
        _efficacyTextFields = CreateEfficacyTextFieldDictionary();
    }

    public void RegisterElementEfficacies(CharacterStats stats)
    {
        _stats = stats;

        DisplayElementEfficacies();
    }

    public void ClearEfficaciesUI()
    {
        _stats = null;
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
            { ElementType.Wind, _windEfficacyField },
            { ElementType.Water, _waterEfficacyField }
        };
    }

    private void DisplayElementEfficacies()
    {
        UpdateEfficacyDisplay(ElementType.Fire);
        UpdateEfficacyDisplay(ElementType.Earth);
        UpdateEfficacyDisplay(ElementType.Wind);
        UpdateEfficacyDisplay(ElementType.Water);
    }
}