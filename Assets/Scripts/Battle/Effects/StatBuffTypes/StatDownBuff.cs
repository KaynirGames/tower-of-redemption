using UnityEngine;

[CreateAssetMenu(fileName = "Stat Down", menuName = "Scriptable Objects/Battle/Effects/Stat Buff Types/Stat Down")]
public class StatDownBuff : StatBuffType
{
    public override StatModifier CreateStatModifier(float buffPower, CharacterStats stats)
    {
        Stat stat = stats.GetStat(_statType);

        return new StatModifier(-stat.BaseValue * buffPower);
    }
}
