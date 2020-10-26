using UnityEngine;

/// <summary>
/// Текстовые данные о стате персонажа.
/// </summary>
[CreateAssetMenu(fileName = "StatName Stat Data", menuName = "Scriptable Objects/Text Data/Stat Data")]
public class StatData : ScriptableObject
{
    [SerializeField] private StatType _statType = StatType.MaxHealth;
    [SerializeField] private float _minValue = 0;
    [SerializeField] private float _maxValue = 999;
    [SerializeField] private TranslatedText _statName = new TranslatedText("Stat.StatID.Name");
    [SerializeField] private TranslatedText _statShrinkName = new TranslatedText("Stat.StatID.ShrinkName");

    public StatType StatType => _statType;
    public float MinValue => _minValue;
    public float MaxValue => _maxValue;
    public string StatName => _statName.Value;
    public string StatShrinkName => _statShrinkName.Value;
}