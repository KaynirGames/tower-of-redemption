using UnityEngine;

/// <summary>
/// Текстовые данные о стате персонажа.
/// </summary>
[CreateAssetMenu(fileName = "StatName Stat Data", menuName = "Scriptable Objects/Text Data/Stat Data")]
public class StatData : ScriptableObject
{
    [SerializeField] private StatType _statType = StatType.MaxHealth;
    [SerializeField] private TranslatedText _statName = new TranslatedText("Stat.StatID.Name");
    [SerializeField] private TranslatedText _statShrinkName = new TranslatedText("Stat.StatID.ShrinkName");

    public StatType StatType => _statType;
    public string StatName => _statName.Value;
    public string StatShrinkName => _statShrinkName.Value;
}