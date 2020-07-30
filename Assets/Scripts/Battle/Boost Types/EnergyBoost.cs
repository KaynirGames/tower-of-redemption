using UnityEngine;

[CreateAssetMenu(fileName = "NewEnergyBoost", menuName = "Scriptable Objects/Battle/Boost Types/Energy Boost")]
public class EnergyBoost : BoostType
{
    public override void AddBoost(CharacterStats target, StatModifier modifier)
    {
        target.MaxEnergy.AddModifier(modifier);
    }

    public override void RemoveBoost(CharacterStats target, StatModifier modifier)
    {
        target.MaxEnergy.RemoveModifier(modifier);
    }
}
