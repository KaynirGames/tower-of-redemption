using UnityEngine;

public abstract class BoostType : ScriptableObject
{
    [SerializeField] private string _statName = "Undefined";

    public string StatName => _statName;

    public abstract void AddBoost(CharacterStats target, StatModifier modifier);

    public abstract void RemoveBoost(CharacterStats target, StatModifier modifier);
}
