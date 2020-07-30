using UnityEngine;

[CreateAssetMenu(fileName = "NewEfficacyBoost", menuName = "Scriptable Objects/Battle/Boost Types/Efficacy Boost")]
public class EfficacyBoost : BoostType
{
    [SerializeField] private MagicElement _magicElement = MagicElement.Fire;

    public override void AddBoost(CharacterStats target, StatModifier modifier)
    {
        ElementEfficacy efficacy = target.GetElementEfficacy(_magicElement);
        efficacy.EfficacyRate.AddModifier(modifier);
    }

    public override void RemoveBoost(CharacterStats target, StatModifier modifier)
    {
        ElementEfficacy efficacy = target.GetElementEfficacy(_magicElement);
        efficacy.EfficacyRate.RemoveModifier(modifier);
    }
}
