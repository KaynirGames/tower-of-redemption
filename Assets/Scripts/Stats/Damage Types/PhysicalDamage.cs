using UnityEngine;

/// <summary>
/// Обычный физический урон.
/// </summary>
[CreateAssetMenu(fileName = "NewPhysicalDamage", menuName = "Scriptable Objects/Damage Types/Physical Damage")]
public class PhysicalDamage : DamageType
{
    public override float CalculateDamage(float damage, CharacterStats target)
    {
        float damageTaken = damage * (1 - target.Armor.GetValue() / 100);

        return Mathf.Round(damageTaken);
    }
}
