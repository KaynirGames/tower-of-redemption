using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

/// <summary>
/// Базовые статы персонажа.
/// </summary>
public abstract class BaseStats : ScriptableObject
{
    [Header("Базовые статы персонажа:")]
    [SerializeField] private float _baseHealth = 100;
    [SerializeField] private float _baseEnergy = 100;
    [SerializeField] private float _baseStrength = 0;
    [SerializeField] private float _baseWill = 0;
    [SerializeField] private float _baseDefence = 0;
    [SerializeField] private float _baseMagicDefence = 0;

    [Header("Эффективность стихий против персонажа:")]
    [SerializeField] private int _baseFireEfficacy = 100;
    [SerializeField] private int _baseAirEfficacy = 100;
    [SerializeField] private int _baseEarthEfficacy = 100;
    [SerializeField] private int _baseWaterEfficacy = 100;

    [Header("Базовые умения персонажа:")]
    [SerializeField] private List<Skill> _baseSkills = new List<Skill>();
    /// <summary>
    /// Базовое количество здоровья.
    /// </summary>
    public float BaseHealth => _baseHealth;
    /// <summary>
    /// Базовое количество духовной энергии.
    /// </summary>
    public float BaseEnergy => _baseEnergy;
    /// <summary>
    /// Базовая сила.
    /// </summary>
    public float BaseStrength => _baseStrength;
    /// <summary>
    /// Базовая воля.
    /// </summary>
    public float BaseWill => _baseWill;
    /// <summary>
    /// Базовая физическая защита.
    /// </summary>
    public float BaseDefence => _baseDefence;
    /// <summary>
    /// Базовая защита от магии.
    /// </summary>
    public float BaseMagicDefence => _baseMagicDefence;
    /// <summary>
    /// Базовая эффективность стихии огня.
    /// </summary>
    public int BaseFireEfficacy => _baseFireEfficacy;
    /// <summary>
    /// Базовая эффективность стихии воздуха.
    /// </summary>
    public int BaseAirEfficacy => _baseAirEfficacy;
    /// <summary>
    /// Базовая эффективность стихии земли.
    /// </summary>
    public int BaseEarthEfficacy => _baseEarthEfficacy;
    /// <summary>
    /// Базовая эффективность стихии воды.
    /// </summary>
    public int BaseWaterEfficacy => _baseWaterEfficacy;
    /// <summary>
    /// Базовый набор умений.
    /// </summary>
    public ReadOnlyCollection<Skill> BaseSkills => _baseSkills.AsReadOnly();
}
