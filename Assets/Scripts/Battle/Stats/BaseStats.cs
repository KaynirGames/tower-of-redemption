using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Базовые статы персонажа.
/// </summary>
public abstract class BaseStats : ScriptableObject
{
    [Header("Базовые статы персонажа:")]
    [SerializeField] private float _baseHealth = 100;
    [SerializeField] private float _baseEnergy = 100;
    [SerializeField] private float _baseArmor = 0;
    [SerializeField] private float _baseMagicDefence = 0;

    [Header("Эффективность стихий против персонажа:")]
    [SerializeField] private float _baseFireEfficacy = 100;
    [SerializeField] private float _baseAirEfficacy = 100;
    [SerializeField] private float _baseEarthEfficacy = 100;
    [SerializeField] private float _baseWaterEfficacy = 100;

    /// <summary>
    /// Базовое количество здоровья.
    /// </summary>
    public float BaseHealth => _baseHealth;
    /// <summary>
    /// Базовое количество очков энергии.
    /// </summary>
    public float BaseEnergy => _baseEnergy;
    /// <summary>
    /// Базовое количество брони.
    /// </summary>
    public float BaseArmor => _baseArmor;
    /// <summary>
    /// Базовая защита от магии.
    /// </summary>
    public float BaseMagicDefence => _baseMagicDefence;
    /// <summary>
    /// Базовая эффективность стихии огня.
    /// </summary>
    public float BaseFireEfficacy => _baseFireEfficacy;
    /// <summary>
    /// Базовая эффективность стихии воздуха.
    /// </summary>
    public float BaseAirEfficacy => _baseAirEfficacy;
    /// <summary>
    /// Базовая эффективность стихии земли.
    /// </summary>
    public float BaseEarthEfficacy => _baseEarthEfficacy;
    /// <summary>
    /// Базовая эффективность стихии воды.
    /// </summary>
    public float BaseWaterEfficacy => _baseWaterEfficacy;
}
