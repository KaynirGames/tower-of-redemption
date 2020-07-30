using UnityEngine;

[CreateAssetMenu(fileName = "NewHealthBoost", menuName = "Scriptable Objects/Battle/Boost Types/Health Boost")]
public class HealthBoost : BoostType
{
    public override void AddBoost(CharacterStats target, StatModifier modifier)
    {
        target.MaxHealth.AddModifier(modifier);
    }

    public override void RemoveBoost(CharacterStats target, StatModifier modifier)
    {
        target.MaxHealth.RemoveModifier(modifier);
    }
}
