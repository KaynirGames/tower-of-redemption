using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    /// <summary>
    /// Событие, информирующее подписчиков о гибели персонажа.
    /// </summary>
    public event Action OnCharacterDeath;
    /// <summary>
    /// Текущее количество очков здоровья.
    /// </summary>
    public float CurrentHealth { get; private set; }
    /// <summary>
    /// Текущее количество очков энергии.
    /// </summary>
    public float CurrentEnergy { get; private set; }

    private Stat _maxHealth; // Максимальное количество очков здоровья.
    private Stat _maxEnergy; // Максимальное количество очков энергии.
    private Stat _armor; // Показатель брони.
    private Stat _magicDefence; // Показатель магической защиты.
    private List<ElementEfficacy> _elementEfficacies; // Эффективности воздействия стихий на персонажа.

    /// <summary>
    /// Задать статы для текущей специализации персонажа.
    /// </summary>
    public void SetStats(BaseStats currentSpec)
    {
        _maxHealth = new Stat(currentSpec.BaseHealth);
        _maxEnergy = new Stat(currentSpec.BaseAbilityPoints);
        _armor = new Stat(currentSpec.BaseArmor);
        _magicDefence = new Stat(currentSpec.BaseMagicDefence);
        _elementEfficacies = new List<ElementEfficacy>()
        {
            new ElementEfficacy(currentSpec.BaseFireEfficacy, MagicElement.Fire),
            new ElementEfficacy(currentSpec.BaseAirEfficacy, MagicElement.Air),
            new ElementEfficacy(currentSpec.BaseEarthEfficacy, MagicElement.Earth),
            new ElementEfficacy(currentSpec.BaseWaterEfficacy, MagicElement.Water)
        };
        CurrentHealth = _maxHealth.GetValue();
        CurrentEnergy = _maxEnergy.GetValue();
    }
    /// <summary>
    /// Получить физический урон текущему здоровью.
    /// </summary>
    public float TakeDamage(float damage)
    {
        float damageTaken = CalculateDamageTaken(damage);
        return ApplyDamageTaken(damageTaken);
    }
    /// <summary>
    /// Получить магический урон текущему здоровью.
    /// </summary>
    public float TakeDamage(float damage, MagicElement element)
    {
        float damageTaken = CalculateDamageTaken(damage, element);
        return ApplyDamageTaken(damageTaken);
    }
    /// <summary>
    /// Достаточно ли текущих очков энергии для применения способности?
    /// </summary>
    public bool IsEnoughEnergy(int energyCost)
    {
        CurrentEnergy -= energyCost;

        return (CurrentEnergy < 0) ? false : true;
    }
    /// <summary>
    /// Применить рассчитанный урон к текущему здоровью.
    /// </summary>
    private float ApplyDamageTaken(float damageTaken)
    {
        if (damageTaken < 0) damageTaken = Mathf.Max(damageTaken, 0);

        CurrentHealth -= damageTaken;

        if (CurrentHealth <= 0)
        {
            OnCharacterDeath?.Invoke();
        }

        return damageTaken;
    }
    /// <summary>
    /// Рассчитать полученный физический урон.
    /// </summary>
    private float CalculateDamageTaken(float damage)
    {
        float damageTaken = damage * (1 - _armor.GetValue() / 100);

        return Mathf.Round(damageTaken);
    }
    /// <summary>
    /// Рассчитать полученный магический урон.
    /// </summary>
    private float CalculateDamageTaken(float damage, MagicElement element)
    {
        ElementEfficacy currentEfficacy = _elementEfficacies.Find(efficacy => efficacy.Element == element);
        float damageTaken = damage * (1 - _magicDefence.GetValue() / 100) * (currentEfficacy.EfficacyRate.GetValue() / 100);

        return Mathf.Round(damageTaken);
    }
}
