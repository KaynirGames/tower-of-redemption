using UnityEngine;

[CreateAssetMenu(fileName = "Small Up Grade", menuName = "Scriptable Objects/Battle/Effects/Stat Buff Grade")]
public class StatBuffGrade : ScriptableObject
{
    [SerializeField] private float _buffRate = 1.15f;
    [SerializeField] private int _priority = 1;
    [SerializeField] private TranslatedText _displayFormat = null;
    [SerializeField] private Sprite _effectIcon = null;

    public bool IsPositive => _buffRate > 0 ? true : false;
    public int Priority => _priority;

    public Sprite EffectIcon => _effectIcon;

    public float CalculateBuffValue(float baseStatValue)
    {
        return baseStatValue * _buffRate;
    }

    public string GetDescription(string statName, string targetType, float effectDuration)
    {
        return string.Format(_displayFormat.Value,
                             statName,
                             targetType,
                             effectDuration);
    }
}