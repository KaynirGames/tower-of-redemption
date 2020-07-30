using UnityEngine;

[CreateAssetMenu(fileName = "NewMagicDefenceBoost", menuName = "Scriptable Objects/Battle/Boost Types/Magic Defence Boost")]
public class MagicDefenceBoost : BoostType
{
    public override void AddBoost(CharacterStats target, StatModifier modifier)
    {
        target.MagicDefence.AddModifier(modifier);
    }

    public override void RemoveBoost(CharacterStats target, StatModifier modifier)
    {
        target.MagicDefence.RemoveModifier(modifier);
    }
}
