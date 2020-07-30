using UnityEngine;

/// <summary>
/// Чистый физический урон, игнорирует броню.
/// </summary>
[CreateAssetMenu(fileName = "NewPurePhysicalDamage", menuName = "Scriptable Objects/Battle/Damage Types/Pure Physical Damage")]
public class PurePhysicalDamage : DamageType
{
    public override float CalculateDamage(float damage, CharacterStats target)
    {
        return Mathf.Round(damage);
    }
}
