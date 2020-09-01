using System.Text;
using UnityEngine;

/// <summary>
/// Эффект, накладываемый на персонажа.
/// </summary>
public abstract class Effect : ScriptableObject
{    
    public abstract void Apply(CharacterStats target);

    public abstract void Remove(CharacterStats target);

    public abstract void BuildDescription(StringBuilder stringBuilder);
}