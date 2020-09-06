using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public event Action<StatType> OnStatChange = delegate { };
    public event Action OnCharacterDeath = delegate { };

    public CharacterResource Health { get; private set; }
    public CharacterResource Energy { get; private set; }

    private Stat _strength;
    private Stat _will;
    private Stat _defence;
    private Stat _magicDefence;

    private Dictionary<StatType, Stat> _statDictionary;
    private Dictionary<ElementType, float> _elementEfficacyDictionary;

    public void SetCharacterStats(SpecBase spec)
    {
        CreateStats(spec);

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

    public void AddStatModifier(StatType statType, StatModifier modifier)
    {
        GetStat(statType).AddModifier(modifier);
        UpdateStatDisplay(statType);
    }

    public void RemoveStatModifier(StatType statType, StatModifier modifier)
    {
        GetStat(statType).RemoveModifier(modifier);
        UpdateStatDisplay(statType);
    }

    public bool IsEnoughEnergy(float energyCost)
    {
        return Energy.CurrentValue - energyCost >= 0;
    }

    private void CreateStats(SpecBase spec)
    {
        Health = new CharacterResource(spec.BaseHealth, spec.BaseHealth);
        Energy = new CharacterResource(spec.BaseEnergy, 0);

        _strength = new Stat(spec.BaseStrength);
        _will = new Stat(spec.BaseWill);
        _defence = new Stat(spec.BaseDefence);
        _magicDefence = new Stat(spec.BaseMagicDefence);
    }

    private Dictionary<StatType, Stat> CreateStatDictionary(SpecBase spec)
    {
        return new Dictionary<StatType, Stat>()
        {
            { StatType.MaxHealth, Health.MaxValue },
            { StatType.MaxEnergy, Energy.MaxValue },
            { StatType.Strength, _strength },
            { StatType.Will, _will },
            { StatType.Defence, _defence },
            { StatType.MagicDefence, _magicDefence }
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

    private void UpdateStatDisplay(StatType statType)
    {
        switch (statType)
        {
            default:
                OnStatChange.Invoke(statType);
                break;
            case StatType.MaxHealth:
                Health.FixCurrentValue(true);
                break;
            case StatType.MaxEnergy:
                Energy.FixCurrentValue(true);
                break;
        }
    }
}
