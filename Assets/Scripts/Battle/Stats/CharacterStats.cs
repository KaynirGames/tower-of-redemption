using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public event Action<StatType> OnStatChange = delegate { };
    public event Action OnCharacterDeath = delegate { };

    public CharacterResource Health { get; private set; }
    public CharacterResource Energy { get; private set; }

    public List<StatBonus> StatBonuses { get; } = new List<StatBonus>();
    public List<Effect> BuffEffects { get; } = new List<Effect>();

    private Dictionary<StatType, Stat> _statDictionary;
    private Dictionary<ElementType, float> _elementEfficacyDictionary;
 
    public void SetCharacterStats(SpecBase spec)
    {
        Health = new CharacterResource(spec.BaseHealth, spec.BaseHealth);
        Energy = new CharacterResource(spec.BaseEnergy, 0);

        _statDictionary = CreateStatDictionary(spec);
        _elementEfficacyDictionary = CreateElementEfficacyDictionary(spec);
    }

    public Stat GetStat(StatType statType)
    {
        return _statDictionary[statType];
    }

    public float GetElementEfficacy(ElementType elementType)
    {
        return _elementEfficacyDictionary[elementType];
    }

    public void ChangeHealth(float healthAmount)
    {
        Health.ChangeResource(healthAmount);

        if (Health.CurrentValue <= 0)
        {
            OnCharacterDeath.Invoke();
        }
    }

    public void ChangeEnergy(float energyAmount)
    {
        Energy.ChangeResource(energyAmount);
    }

    public void UpdateStatDisplay(StatType statType)
    {
        if (statType == StatType.MaxHealth)
        {
            Health.FixCurrentValue();
        } 
        else if (statType == StatType.MaxEnergy)
        {
            Energy.FixCurrentValue();
        }
        else
        {
            OnStatChange.Invoke(statType);
        }
    }

    public bool IsEnoughEnergy(float energyCost)
    {
        return Energy.CurrentValue - energyCost >= 0;
    }

    private Dictionary<StatType, Stat> CreateStatDictionary(SpecBase spec)
    {
        return new Dictionary<StatType, Stat>()
        {
            { StatType.MaxHealth, Health.MaxValue },
            { StatType.MaxEnergy, Energy.MaxValue },
            { StatType.Strength, new Stat(spec.BaseStrength) },
            { StatType.Will, new Stat(spec.BaseWill) },
            { StatType.Defence, new Stat(spec.BaseDefence) },
            { StatType.MagicDefence, new Stat(spec.BaseMagicDefence) }
        };
    }

    private Dictionary<ElementType, float> CreateElementEfficacyDictionary(SpecBase spec)
    {
        return new Dictionary<ElementType, float>()
        {
            { ElementType.Fire, spec.BaseFireEfficacy },
            { ElementType.Air, spec.BaseAirEfficacy },
            { ElementType.Earth, spec.BaseEarthEfficacy },
            { ElementType.Water, spec.BaseWaterEfficacy }
        };
    }
}
