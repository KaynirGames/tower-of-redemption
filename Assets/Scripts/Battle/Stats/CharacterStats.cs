using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    /// <summary>
    /// Событие, информирующее подписчиков о гибели персонажа.
    /// </summary>
    public event Action OnCharacterDeath = delegate { };
    /// <summary>
    /// Текущее количество очков здоровья.
    /// </summary>
    public float CurrentHealth { get; private set; }
    /// <summary>
    /// Текущее количество духовной энергии.
    /// </summary>
    public float CurrentEnergy { get; private set; }
    /// <summary>
    /// Максимальное количество очков здоровья.
    /// </summary>
    public Stat MaxHealth { get; private set; }
    /// <summary>
    /// Максимальное количество духовной энергии.
    /// </summary>
    public Stat MaxEnergy { get; private set; }
    /// <summary>
    /// Показатель силы.
    /// </summary>
    public Stat Strength { get; private set; }
    /// <summary>
    /// Показатель воли.
    /// </summary>
    public Stat Will { get; private set; }
    /// <summary>
    /// Показатель физической защиты.
    /// </summary>
    public Stat Defence { get; private set; }
    /// <summary>
    /// Показатель магической защиты.
    /// </summary>
    public Stat MagicDefence { get; private set; }

    private List<ElementEfficacy> _elementEfficacies; // Эффективности воздействия стихий на персонажа.
    private readonly List<SkillEffect> _characterEffects = new List<SkillEffect>(); // Эффекты, действующие на персонажа.

    /// <summary>
    /// Задать базовые статы для текущей специализации персонажа.
    /// </summary>
    public void SetBaseStats(BaseStats currentSpec)
    {
        MaxHealth = new Stat(currentSpec.BaseHealth);
        MaxEnergy = new Stat(currentSpec.BaseEnergy);
        Strength = new Stat(currentSpec.BaseStrength);
        Will = new Stat(currentSpec.BaseWill);
        Defence = new Stat(currentSpec.BaseDefence);
        MagicDefence = new Stat(currentSpec.BaseMagicDefence);
        _elementEfficacies = new List<ElementEfficacy>()
        {
            new ElementEfficacy(currentSpec.BaseFireEfficacy, ElementType.Fire),
            new ElementEfficacy(currentSpec.BaseAirEfficacy, ElementType.Air),
            new ElementEfficacy(currentSpec.BaseEarthEfficacy, ElementType.Earth),
            new ElementEfficacy(currentSpec.BaseWaterEfficacy, ElementType.Water)
        };
        CurrentHealth = MaxHealth.GetValue();
        CurrentEnergy = MaxEnergy.GetValue();
    }
    /// <summary>
    /// Получить урон текущему здоровью.
    /// </summary>
    public float TakeDamage(float damageTaken)
    {
        return ApplyDamageTaken(damageTaken);
    }
    /// <summary>
    /// Получить эффективность воздействия магического элемента.
    /// </summary>
    public ElementEfficacy GetElementEfficacy(ElementType element)
    {
        return _elementEfficacies.Find(efficacy => efficacy.Element == element);
    }
    /// <summary>
    /// Наличие духовной энергии для применения умения.
    /// </summary>
    public bool IsEnoughEnergy(int energyCost)
    {
        CurrentEnergy -= energyCost;

        return (CurrentEnergy < 0) ? false : true;
    }
    /// <summary>
    /// Проверить наличие эффекта на персонаже.
    /// </summary>
    public bool IsEffectExist(SkillEffect effect)
    {
        return _characterEffects.Contains(effect);
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
}
