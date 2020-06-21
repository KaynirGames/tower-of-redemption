using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Базовые статы персонажа.
/// </summary>
public abstract class BaseStats : ScriptableObject
{
    [Header("Базовые статы персонажа:")]
    /// <summary>
    /// Базовое количество здоровья.
    /// </summary>
    public float BaseHealth = 100;
    /// <summary>
    /// Базовое количество очков навыков.
    /// </summary>
    public float BaseAbilityPoints = 100;
    /// <summary>
    /// Базовое количество брони.
    /// </summary>
    public float BaseArmor = 0;
    /// <summary>
    /// Базовая защита от магии.
    /// </summary>
    public float BaseMagicDefence = 0;
    [Header("Эффективность стихий против персонажа:")]
    /// <summary>
    /// Базовая эффективность стихии огня.
    /// </summary>
    public float BaseFireEfficacy = 100;
    /// <summary>
    /// Базовая эффективность стихии воздуха.
    /// </summary>
    public float BaseAirEfficacy = 100;
    /// <summary>
    /// Базовая эффективность стихии земли.
    /// </summary>
    public float BaseEarthEfficacy = 100;
    /// <summary>
    /// Базовая эффективность стихии воды.
    /// </summary>
    public float BaseWaterEfficacy = 100;
}
