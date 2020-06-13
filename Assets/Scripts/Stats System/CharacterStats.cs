using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private PlayerSpec currentSpec = null;
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

    private void Awake()
    {
        SetPlayerSpec(currentSpec);
        CurrentHealth = maxHealth.Value;
        CurrentAbilityPoints = maxAbilityPoints.Value;
    }

    private void OnValidate()
    {
        if (currentSpec != null)
            SetPlayerSpec(currentSpec);
    }
    /// <summary>
    /// Задать текущую специализацию персонажа.
    /// </summary>
    /// <param name="currentSpec"></param>
    public void SetPlayerSpec(PlayerSpec currentSpec)
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
        return damage - armor.Value;
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
        return (damage - magicDefence.Value) * currentEfficacy.EfficacyRate.Value / 100;
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
