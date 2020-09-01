using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Класс для отображения статов персонажа на UI.
/// </summary>
public class StatDisplayUI : MonoBehaviour
{
    [Header("Текстовые поля со статами:")]
    [SerializeField] private TextMeshProUGUI _strengthField = null;
    [SerializeField] private TextMeshProUGUI _defenceField = null;
    [SerializeField] private TextMeshProUGUI _willField = null;
    [SerializeField] private TextMeshProUGUI _magicDefenceField = null;

    private Dictionary<StatType, TextMeshProUGUI> _statTextFields;

    private CharacterStats _stats;

    public void RegisterStats(CharacterStats stats)
    {
        _stats = stats;

        _statTextFields = CreateStatTextFieldDictionary();

        DisplayCharacterStats();

        stats.OnStatChange += UpdateStatDisplay;
    }

    private void UpdateStatDisplay(StatType statType)
    {
        if (_statTextFields.ContainsKey(statType))
        {
            _statTextFields[statType].SetText(_stats.GetStat(statType)
                                                    .GetFinalValue()
                                                    .ToString());
        }
    }

    private Dictionary<StatType, TextMeshProUGUI> CreateStatTextFieldDictionary()
    {
        return new Dictionary<StatType, TextMeshProUGUI>()
        {
            { StatType.Strength, _strengthField },
            { StatType.Defence, _defenceField },
            { StatType.Will, _willField },
            { StatType.MagicDefence, _magicDefenceField }
        };
    }

    private void DisplayCharacterStats()
    {
        UpdateStatDisplay(StatType.Strength);
        UpdateStatDisplay(StatType.Defence);
        UpdateStatDisplay(StatType.Will);
        UpdateStatDisplay(StatType.MagicDefence);
    }

    private void OnDestroy()
    {
        _stats.OnStatChange -= UpdateStatDisplay;
    }
}
