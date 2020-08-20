using TMPro;
using UnityEngine;

/// <summary>
/// Класс для отображения статов персонажа.
/// </summary>
public class StatDisplayUI : MonoBehaviour
{
    [Header("Текстовые поля со статами:")]
    [SerializeField] private TextMeshProUGUI _strengthField = null;
    [SerializeField] private TextMeshProUGUI _defenceField = null;
    [SerializeField] private TextMeshProUGUI _willField = null;
    [SerializeField] private TextMeshProUGUI _magicDefenceField = null;

    private CharacterStats _characterStats; // Текущие статы персонажа.
    /// <summary>
    /// Инициализация отображения статов персонажа.
    /// </summary>
    public void Init(CharacterStats characterStats)
    {
        _characterStats = characterStats;

        DisplayStats(characterStats);

        characterStats.OnStatChange += UpdateStatsDisplay;
    }
    /// <summary>
    /// Обновить отображение статов.
    /// </summary>
    public void UpdateStatsDisplay()
    {
        DisplayStats(_characterStats);
    }
    /// <summary>
    /// Выставить текст со статами.
    /// </summary>
    private void DisplayStats(CharacterStats characterStats)
    {
        _strengthField.SetText(characterStats.Strength
            .GetValue().ToString());

        _defenceField.SetText(characterStats.Defence
            .GetValue().ToString());

        _willField.SetText(characterStats.Will
            .GetValue().ToString());

        _magicDefenceField.SetText(characterStats.MagicDefence
            .GetValue().ToString());
    }

    private void OnDestroy()
    {
        _characterStats.OnStatChange -= UpdateStatsDisplay;
    }
}
