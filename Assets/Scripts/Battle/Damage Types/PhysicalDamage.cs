using UnityEngine;

/// <summary>
/// Обычный физический урон.
/// </summary>
[CreateAssetMenu(fileName = "NewPhysicalDamage", menuName = "Scriptable Objects/Battle/Damages/Physical Damage")]
public class PhysicalDamage : Damage
{
    [SerializeField] private int _minDamage = 0; // Минимальный урон.
    [SerializeField] private int _maxDamage = 0; // Максимальный урон.

    public override float CalculateDamage(CharacterStats target)
    {
        float damage = Random.Range(_minDamage, _maxDamage);
        float damageTaken = damage * (1 - target.Defence.GetValue() / 100f);

        return Mathf.Round(damageTaken);
    }

    public override string GetDescription()
    {
        return string.Format("{0}-{1} Физический",
            _minDamage, _maxDamage);
    }
}
