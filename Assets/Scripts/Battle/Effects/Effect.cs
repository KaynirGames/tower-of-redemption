using System.Text;
using UnityEngine;

/// <summary>
/// Эффект, накладываемый на персонажа.
/// </summary>
public abstract class Effect : ScriptableObject
{
    public abstract void Apply(Character target);

    public abstract void Remove(Character target);

    public abstract void BuildDescription(StringBuilder builder);
}