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

    private FloatingTextPopup _damageTextPopup;
    private Character _character;

    private void Awake()
    {
        _character = GetComponent<Character>();
    }

    public void SetCharacterStats(SpecBase spec)
    {
        CreateCharacterStats(spec);

        _statDictionary = CreateStatDictionary();
        _elementEfficacyDictionary = CreateElementEfficacyDictionary(spec);

        _damageTextPopup = BattleManager.Instance.DamageTextPopup;
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

        if (healthAmount < 0)
        {
            ShowDamageTextPopup(healthAmount);
        }

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

    public void RemoveStatModifier(StatType statType, object modifierSource)
    {
        GetStat(statType).RemoveModifiers(modifierSource);
        UpdateStatDisplay(statType);
    }

    public bool IsEnoughEnergy(float energyCost)
    {
        return Energy.CurrentValue - energyCost >= 0;
    }

    private void CreateCharacterStats(SpecBase spec)
    {
        Health = new CharacterResource(CreateStat(spec.BaseHealth, StatType.MaxHealth), spec.BaseHealth);
        Energy = new CharacterResource(CreateStat(spec.BaseEnergy, StatType.MaxEnergy), 0);

        _strength = CreateStat(spec.BaseStrength, StatType.Strength);
        _will = CreateStat(spec.BaseWill, StatType.Will);
        _defence = CreateStat(spec.BaseDefence, StatType.Defence);
        _magicDefence = CreateStat(spec.BaseMagicDefence, StatType.MagicDefence);
    }

    private Stat CreateStat(float baseValue, StatType statType)
    {
        StatSO statData = DatabaseManager.Instance.StatDatabase.Find(x => x.StatType == statType);
        Stat stat = new Stat(baseValue, statData.MinValue, statData.MaxValue);

        return stat;
    }

    private Dictionary<StatType, Stat> CreateStatDictionary()
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
            { ElementType.Wind, spec.BaseAirEfficacy },
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

    private void ShowDamageTextPopup(float damageTaken)
    {
        float fontSize = damageTaken <= -10f
            ? _damageTextPopup.FontSize - Mathf.Round(damageTaken / 10f)
            : _damageTextPopup.FontSize;

        _damageTextPopup.Create(damageTaken.ToString(),
                                transform.position,
                                fontSize);
    }
}
