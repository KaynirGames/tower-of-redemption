using UnityEngine;

/// <summary>
/// Тип наносимого урона.
/// </summary>
public abstract class DamageSO : ScriptableObject
{
    public abstract float CalculateDamage(Character owner,
                                          Character opponent,
                                          float attackPower);

    public abstract float CalculateDamage(Character target,
                                          float baseDamage);
}
