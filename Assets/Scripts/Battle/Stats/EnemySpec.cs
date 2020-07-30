using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Специализация противника.
/// </summary>
[CreateAssetMenu(fileName = "NewEnemySpec", menuName = "Scriptable Objects/Battle/Specs/Enemy Spec")]
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
    public float AbilityPointRegenOverTime = 1f;
    /// <summary>
    /// Отрезок времени, спустя который регенерируются очки навыков.
    /// </summary>
    public float AbilityPointRegenTime = 1f;
    /// <summary>
    /// Таблица вероятностей выпадения лута.
    /// </summary>
    public SpawnTable LootTable;
    [Header("Основные настройки ИИ:")]
    /// <summary>
    /// Скорость перемещения.
    /// </summary>
    public float MoveSpeed = 2f;
    /// <summary>
    /// Дальность обзора.
    /// </summary>
    public float ViewDistance = 4f;
    /// <summary>
    /// Расстояние атаки.
    /// </summary>
    public float AttackDistance = 2f;
    /// <summary>
    /// Задержка между атаками.
    /// </summary>
    public float AttackCooldown = 1f;
}
