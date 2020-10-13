using UnityEngine;

[CreateAssetMenu(fileName = "StatID Stat Data", menuName = "Scriptable Objects/Battle/Stat Data")]
public class StatData : ScriptableObject
{
    [SerializeField] private StatType _statType = StatType.MaxHealth;
    [SerializeField] private TranslatedText _statName = new TranslatedText("Stat.StatID.Name");

    public StatType StatType => _statType;
    public string StatName => _statName.Value;
}