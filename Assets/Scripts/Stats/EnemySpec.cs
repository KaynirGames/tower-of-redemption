using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Специализация противника.
/// </summary>
[CreateAssetMenu(fileName = "NewEnemySpec", menuName = "Scriptable Objects/Stats System/Enemy Spec")]
public class EnemySpec : BaseStats
{
    [Header("Информация о спеке противника:")]
    /// <summary>
    /// Имя противника, отображаемое в бою.
    /// </summary>
    public string DisplayName = "Unknown";
    /// <summary>
    /// Количество очков навыков, которые регенерируются каждый отрезок времени.
    /// </summary>
    public float AbilityPointRegenOverTime;
    /// <summary>
    /// Отрезок времени, спустя который регенерируются очки навыков.
    /// </summary>
    public float AbilityPointRegenTime;
    /// <summary>
    /// Таблица вероятностей выпадения лута.
    /// </summary>
    public SpawnTable LootTable;
}
