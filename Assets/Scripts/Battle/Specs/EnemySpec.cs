using UnityEngine;

/// <summary>
/// Специализация противника.
/// </summary>
[CreateAssetMenu(fileName = "NewEnemySpec", menuName = "Scriptable Objects/Battle/Specs/Enemy Spec")]
public class EnemySpec : SpecBase
{
    [Header("Информация о спеке противника:")]
    [SerializeField] private float _energyRegen = 1f;
    [SerializeField] private float _energyRegenDelay = 1f;

    public float EnergyRegen => _energyRegen;
    public float EnergyRegenDelay => _energyRegenDelay;
}
