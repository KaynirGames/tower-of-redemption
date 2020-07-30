using UnityEngine;

[CreateAssetMenu(fileName = "NewArmorBoost", menuName = "Scriptable Objects/Battle/Boost Types/Armor Boost")]
public class ArmorBoost : BoostType
{
    public override void AddBoost(CharacterStats target, StatModifier modifier)
    {
        target.Armor.AddModifier(modifier);
    }

    public override void RemoveBoost(CharacterStats target, StatModifier modifier)
    {
        target.Armor.RemoveModifier(modifier);
    }
}
