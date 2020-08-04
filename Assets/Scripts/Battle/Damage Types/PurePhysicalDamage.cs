using UnityEngine;

/// <summary>
/// Чистый физический урон, игнорирует броню.
/// </summary>
[CreateAssetMenu(fileName = "NewPurePhysicalDamage", menuName = "Scriptable Objects/Battle/Damages/Pure Physical Damage")]
public class PurePhysicalDamage : Damage
{
    [SerializeField] private int _minDamage = 0; // Минимальный урон.
    [SerializeField] private int _maxDamage = 0; // Максимальный урон.

    public override float CalculateDamage(CharacterStats target)
    {
        float damage = Random.Range(_minDamage, _maxDamage);

        return Mathf.Round(damage);
    }

    public override string GetDescription()
    {
        return string.Format("{0}-{1} Физический (Чистый)",
            _minDamage, _maxDamage);
    }
}
