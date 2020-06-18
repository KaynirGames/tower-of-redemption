using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    /// <summary>
    /// Максимальное количество здоровья.
    /// </summary>
    private Stat maxHealth;
    /// <summary>
    /// Максимальное количество очков навыков.
    /// </summary>
    private Stat maxAbilityPoints;
    /// <summary>
    /// Количество брони (физическая защита).
    /// </summary>
    private Stat armor;
    /// <summary>
    /// Магическая защита.
    /// </summary>
    private Stat magicDefence;
    /// <summary>
    /// Список эффективности воздействия стихийных элементов на персонажа.
    /// </summary>
    private List<ElementEfficacy> elementEfficacies;
    /// <summary>
    /// Текущее количество здоровья.
    /// </summary>
    public float CurrentHealth { get; private set; }
    /// <summary>
    /// Текущее количество очков навыков.
    /// </summary>
    public float CurrentAbilityPoints { get; private set; }
    /// <summary>
    /// Делегат, сообщающий о гибели персонажа.
    /// </summary>
    public Action OnCharacterDeath;

    /// <summary>
    /// Задать статы для текущей специализации персонажа.
    /// </summary>
    /// <param name="currentSpec">Текущая специализация персонажа.</param>
    public void SetCharacterStats(BaseStats currentSpec)
    {
        maxHealth = new Stat(currentSpec.BaseHealth);
        maxAbilityPoints = new Stat(currentSpec.BaseAbilityPoints);
        armor = new Stat(currentSpec.BaseArmor);
        magicDefence = new Stat(currentSpec.BaseMagicDefence);
        elementEfficacies = new List<ElementEfficacy>()
        {
            new ElementEfficacy(currentSpec.BaseFireEfficacy, MagicElement.Fire),
            new ElementEfficacy(currentSpec.BaseAirEfficacy, MagicElement.Air),
            new ElementEfficacy(currentSpec.BaseEarthEfficacy, MagicElement.Earth),
            new ElementEfficacy(currentSpec.BaseWaterEfficacy, MagicElement.Water)
        };
        CurrentHealth = maxHealth.Value;
        CurrentAbilityPoints = maxAbilityPoints.Value;
    }
    /// <summary>
    /// Получить урон текущему здоровью.
    /// </summary>
    /// <param name="damage">Количество урона.</param>
    /// <param name="element">Стихийный элемент (отсутствует у физического урона).</param>
    public void TakeDamage(float damage, MagicElement element)
    {
        if (element == MagicElement.None)
        {
            damage = CalculateDamageTaken(damage);
        }
        else
        {
            damage = CalculateDamageTaken(damage, element);
        }

        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            OnCharacterDeath?.Invoke();
        }
    }
    /// <summary>
    /// Рассчитать полученный физический урон.
    /// </summary>
    /// <param name="damage">Начальное значение.</param>
    /// <returns></returns>
    private float CalculateDamageTaken(float damage)
    {
        float damageTaken = damage * (1 - armor.Value / 100);

        return Mathf.Round(damageTaken);
    }
    /// <summary>
    /// Рассчитать полученный магический урон.
    /// </summary>
    /// <param name="damage">Начальное значение.</param>
    /// /// <param name="element">Стихийный элемент.</param>
    /// <returns></returns>
    private float CalculateDamageTaken(float damage, MagicElement element)
    {
        ElementEfficacy currentEfficacy = elementEfficacies.Find(efficacy => efficacy.Element == element);
        float damageTaken = damage * (1 - magicDefence.Value / 100) * (currentEfficacy.EfficacyRate.Value / 100);

        return Mathf.Round(damageTaken);
    }
    /// <summary>
    /// Достаточно ли текущих очков навыков для применения способности?
    /// </summary>
    /// <param name="abilityCost">Стоимость способности.</param>
    /// <returns></returns>
    public bool IsEnoughAbilityPoints(int abilityCost)
    {
        CurrentAbilityPoints -= abilityCost;

        return (CurrentAbilityPoints < 0) ? false : true;
    }
}
