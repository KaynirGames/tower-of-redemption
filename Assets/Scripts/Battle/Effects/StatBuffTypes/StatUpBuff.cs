using UnityEngine;

[CreateAssetMenu(fileName = "Stat Up", menuName = "Scriptable Objects/Battle/Effects/Stat Buff Types/Stat Up")]
public class StatUpBuff : StatBuffType
{
    public override StatModifier CreateStatModifier(float buffPower, CharacterStats stats)
    {
        Stat stat = stats.GetStat(_statType);

        return new StatModifier(stat.BaseValue * buffPower);
    }
}
