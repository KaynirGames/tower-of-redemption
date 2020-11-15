using UnityEngine;

[CreateAssetMenu(fileName = "Small/Medium/Large Up/Down Grade SO", menuName = "Scriptable Objects/Battle/Effects/Stat Buff Grade SO")]
public class StatBuffGradeSO : ScriptableObject
{
    [SerializeField] private float _buffRate = 0.15f;
    [SerializeField] private BuffGradeType _gradeType = BuffGradeType.Small;

    public bool IsPositive => _buffRate > 0 ? true : false;
    
    public int Priority => (int)_gradeType;

    public float CalculateBuffValue(float baseStatValue)
    {
        return baseStatValue * _buffRate;
    }

    private enum BuffGradeType
    {
        Small = 100,
        Medium = 200,
        Large = 300
    }
}