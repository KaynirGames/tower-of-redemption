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
    [SerializeField] private SpawnTable _lootTable = null;
    [Header("Основные настройки ИИ:")]
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _viewDistance = 4f;

    public float EnergyRegen => _energyRegen;
    public float EnergyRegenDelay => _energyRegenDelay;
    public SpawnTable LootTable => _lootTable;

    public float MoveSpeed => _moveSpeed;
    public float ViewDistance => _viewDistance;
}
