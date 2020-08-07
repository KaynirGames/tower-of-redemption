using UnityEngine;

/// <summary>
/// Физический урон.
/// </summary>
[CreateAssetMenu(fileName = "NewPhysDamage", menuName = "Scriptable Objects/Battle/Damage Types/Physical Damage")]
public class PhysicalDamage : DamageType
{
    public override float CalculateDamage(CharacterStats user, CharacterStats target, PowerTier tier)
    {
        float userPower = user.Strength.GetValue() * tier.PowerModifier;
        float targetDefence = 1 - (target.Defence.GetValue() / 100);

        return Mathf.Round(userPower - targetDefence);
    }
}
