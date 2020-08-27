using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public delegate void OnResourceChange(float currentValue); // Делегат для сообщений об изменении значений ресурса.

    public event OnResourceChange OnHealthChange = delegate { }; // Событие при изменении здоровья.
    public event OnResourceChange OnEnergyChange = delegate { }; // Событие при изменении энергии.
    public event Action OnStatChange = delegate { }; // Событие при изменении стата.
    public event Action OnCharacterDeath = delegate { }; // Событие, информирующее подписчиков о гибели персонажа.
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
    /// <summary>
    /// Эффекты, действующие на персонажа.
    /// </summary>
    public List<SkillEffect> InflictedEffects { get; } = new List<SkillEffect>();
    /// <summary>
    /// Постоянные эффекты, действующие на персонажа.
    /// </summary>
    public List<SkillEffect> PermanentEffects { get; } = new List<SkillEffect>();

    private List<ElementEfficacy> _elementEfficacies; // Эффективности воздействия стихий на персонажа.
    /// <summary>
    /// Задать базовые статы для текущей специализации персонажа.
    /// </summary>
    public void SetBaseStats(SpecBase currentSpec)
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
        CurrentEnergy = 0;
    }
    /// <summary>
    /// Получить урон текущему здоровью.
    /// </summary>
    public void TakeDamage(float damageTaken)
    {
        if (damageTaken < 0) { return; }

        CurrentHealth = Mathf.Clamp(CurrentHealth - damageTaken, 0, MaxHealth.GetValue());

        OnHealthChange.Invoke(CurrentHealth);

        if (CurrentHealth <= 0)
        {
            OnCharacterDeath?.Invoke();
        }
    }
    /// <summary>
    /// Восстановить текущее здоровье.
    /// </summary>
    public void RecoverHealth(float amount)
    {
        if (amount < 0) { return; }

        CurrentHealth = Mathf.Round(CurrentHealth + amount);
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth.GetValue());

        OnHealthChange.Invoke(CurrentHealth);
    }
    /// <summary>
    /// Восстановить текущую духовную энергию.
    /// </summary>
    public void RecoverEnergy(float amount)
    {
        if (amount < 0) { return; }

        CurrentEnergy = Mathf.Round(CurrentEnergy + amount);
        CurrentEnergy = Mathf.Clamp(CurrentEnergy, 0, MaxEnergy.GetValue());

        OnEnergyChange.Invoke(CurrentEnergy);
    }
    /// <summary>
    /// Обновить отображение здоровья персонажа.
    /// </summary>
    public void UpdateResourcesDisplay()
    {
        OnHealthChange.Invoke(CurrentHealth);
        OnEnergyChange.Invoke(CurrentEnergy);
    }
    /// <summary>
    /// Обновить отображение статов персонажа.
    /// </summary>
    public void UpdateStatsDisplay()
    {
        OnStatChange.Invoke();
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
    public bool IsEnoughEnergy(float energyCost)
    {
        return CurrentEnergy - energyCost >= 0;
    }
    /// <summary>
    /// Проверить наличие эффекта на персонаже.
    /// </summary>
    public bool IsEffectExist(SkillEffect effect)
    {
        return InflictedEffects.Contains(effect) || PermanentEffects.Contains(effect);
    }
}
