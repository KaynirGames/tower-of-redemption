using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Базовые статы персонажа.
/// </summary>
public abstract class BaseStats : ScriptableObject
{
    /// <summary>
    /// Базовое количество здоровья.
    /// </summary>
    public float BaseHealth;
    /// <summary>
    /// Базовое количество очков навыков.
    /// </summary>
    public float BaseAbilityPoints;
    /// <summary>
    /// Базовое количество брони.
    /// </summary>
    public float BaseArmor;
    /// <summary>
    /// Базовая защита от магии.
    /// </summary>
    public float BaseMagicDefence;
    /// <summary>
    /// Базовая эффективность стихии огня.
    /// </summary>
    public float BaseFireEfficacy;
    /// <summary>
    /// Базовая эффективность стихии воздуха.
    /// </summary>
    public float BaseAirEfficacy;
    /// <summary>
    /// Базовая эффективность стихии земли.
    /// </summary>
    public float BaseEarthEfficacy;
    /// <summary>
    /// Базовая эффективность стихии воды.
    /// </summary>
    public float BaseWaterEfficacy;
}
