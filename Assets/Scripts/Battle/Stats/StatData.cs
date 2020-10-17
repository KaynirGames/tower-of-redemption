using UnityEngine;

[CreateAssetMenu(fileName = "StatID Stat Data", menuName = "Scriptable Objects/Battle/Stat Data")]
public class StatData : ScriptableObject
{
    [SerializeField] private StatType _statType = StatType.MaxHealth;
    [SerializeField] private TranslatedText _statName = new TranslatedText("Stat.StatID.Name");
    [SerializeField] private TranslatedText _statShrinkName = new TranslatedText("Stat.StatID.ShrinkName");
    [SerializeField] private Color _positiveStatColor = new Color(65, 119, 50, 255);
    [SerializeField] private Color _negativeStatColor = new Color(126, 45, 57, 255);


    public StatType StatType => _statType;
    public string StatName => _statName.Value;
    public string StatShrinkName => _statShrinkName.Value;

    public string GetRichTextColor(bool isPositiveStat)
    {
        return isPositiveStat
            ? ColorUtility.ToHtmlStringRGBA(_positiveStatColor)
            : ColorUtility.ToHtmlStringRGBA(_negativeStatColor);
    }
}