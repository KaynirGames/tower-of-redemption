using TMPro;
using UnityEngine;

public class EfficacyDisplayUI : MonoBehaviour
{
    [Header("Текстовые поля с эффективностью элементов:")]
    [SerializeField] private TextMeshProUGUI _fireEfficacyField = null;
    [SerializeField] private TextMeshProUGUI _earthEfficacyField = null;
    [SerializeField] private TextMeshProUGUI _airEfficacyField = null;
    [SerializeField] private TextMeshProUGUI _waterEfficacyField = null;

    private CharacterStats _characterStats; // Текущие статы персонажа.
    /// <summary>
    /// Инициализация отображения эффективности элементов.
    /// </summary>
    public void Init(CharacterStats characterStats)
    {
        _characterStats = characterStats;

        DisplayEfficacy(characterStats);
    }
    /// <summary>
    /// Отобразить эффективность элементов.
    /// </summary>
    private void DisplayEfficacy(CharacterStats characterStats)
    {
        _fireEfficacyField.SetText(characterStats.GetElementEfficacy(ElementType.Fire)
            .EfficacyRate.ToString());

        _earthEfficacyField.SetText(characterStats.GetElementEfficacy(ElementType.Earth)
            .EfficacyRate.ToString());

        _airEfficacyField.SetText(characterStats.GetElementEfficacy(ElementType.Air)
            .EfficacyRate.ToString());

        _waterEfficacyField.SetText(characterStats.GetElementEfficacy(ElementType.Water)
            .EfficacyRate.ToString());
    }
}
