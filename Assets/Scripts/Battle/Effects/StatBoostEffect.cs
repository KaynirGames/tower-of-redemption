using UnityEngine;

[CreateAssetMenu(fileName = "NewStatBoostEffect", menuName = "Scriptable Objects/Battle/Effects/Stat Boost Effect")]
public class StatBoostEffect : Effect
{
    [SerializeField] private StatModifier _statModifier = null;
    [SerializeField] private BoostType _boostType = null;

    public override void Apply(CharacterStats target)
    {
        _boostType.AddBoost(target, _statModifier);
    }

    public override string GetDisplayInfo()
    {
        string displayInfo = string.Concat(
            _boostType.StatName, " + ", _statModifier.Value, " ед."
            );
        return displayInfo;
    }
}
