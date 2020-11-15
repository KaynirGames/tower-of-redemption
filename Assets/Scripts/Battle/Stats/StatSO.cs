using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "Undefined Stat SO", menuName = "Scriptable Objects/Battle/Stat SO")]
public class StatSO : ScriptableObject
{
    [SerializeField] private StatType _statType = StatType.MaxHealth;
    [SerializeField] private float _minValue = 0;
    [SerializeField] private float _maxValue = 999;
    [SerializeField] private LocalizedString _statName = null;
    [SerializeField] private LocalizedString _statShrinkName = null;

    public StatType StatType => _statType;
    public float MinValue => _minValue;
    public float MaxValue => _maxValue;
    public string StatName => _statName.GetLocalizedString().Result;
    public string StatShrinkName => _statShrinkName.GetLocalizedString().Result;
}