using UnityEngine;

/// <summary>
/// Специализация противника.
/// </summary>
[CreateAssetMenu(fileName = "NewEnemySpec", menuName = "Scriptable Objects/Battle/Specs/Enemy Spec")]
public class EnemySpec : BaseStats
{
    [Header("Информация о спеке противника:")]
    [SerializeField] private TranslatedText _nameText = null;
    [SerializeField] private float _energyRegen = 1f;
    [SerializeField] private float _energyRegenDelay = 1f;
    [SerializeField] private SpawnTable _lootTable = null;
    [Header("Основные настройки ИИ:")]
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _viewDistance = 4f;
    /// <summary>
    /// Имя противника, отображаемое в бою.
    /// </summary
    public string Name => _nameText.Value;
    /// <summary>
    /// Энергия, восстанавливаемая каждый отрезок времени.
    /// </summary>
    public float EnergyRegen => _energyRegen;
    /// <summary>
    /// Задержка между восстановлениями энергии.
    /// </summary>
    public float EnergyRegenDelay => _energyRegen;
    /// <summary>
    /// Таблица вероятностей выпадения лута.
    /// </summary>
    public SpawnTable LootTable => _lootTable;
    /// <summary>
    /// Скорость перемещения.
    /// </summary>
    public float MoveSpeed => _moveSpeed;
    /// <summary>
    /// Дальность обзора.
    /// </summary>
    public float ViewDistance => _viewDistance;
}
