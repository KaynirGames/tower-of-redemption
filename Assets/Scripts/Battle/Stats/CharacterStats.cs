using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public event Action<StatType> OnStatChange = delegate { };
    public event Action OnCharacterDeath = delegate { };

    public CharacterResource Health { get; private set; }
    public CharacterResource Spirit { get; private set; }

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

        _damageTextPopup = AssetManager.Instance.DamageTextPopup;
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

    public void ChangeSpirit(float spiritAmount)
    {
        Spirit.ChangeResource(spiritAmount);
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
        return Spirit.CurrentValue - energyCost >= 0;
    }

    private void CreateCharacterStats(SpecBase spec)
    {
        Health = new CharacterResource(CreateStat(spec.BaseHealth, StatType.MaxHealth), spec.BaseHealth);
        Spirit = new CharacterResource(CreateStat(spec.BaseSpirit, StatType.MaxSpirit), 0);

        _strength = CreateStat(spec.BaseStrength, StatType.Strength);
        _will = CreateStat(spec.BaseWill, StatType.Will);
        _defence = CreateStat(spec.BaseDefence, StatType.Defence);
        _magicDefence = CreateStat(spec.BaseMagicDefence, StatType.MagicDefence);
    }

    private Stat CreateStat(float baseValue, StatType statType)
    {
        StatSO statData = AssetManager.Instance.StatDatabase.Find(x => x.StatType == statType);
        Stat stat = new Stat(baseValue, statData.MinValue, statData.MaxValue);

        return stat;
    }

    private Dictionary<StatType, Stat> CreateStatDictionary()
    {
        return new Dictionary<StatType, Stat>()
        {
            { StatType.MaxHealth, Health.MaxValue },
            { StatType.MaxSpirit, Spirit.MaxValue },
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
            case StatType.MaxSpirit:
                Spirit.FixCurrentValue(true);
                break;
        }
    }

    private void ShowDamageTextPopup(float damageTaken)
    {
        _damageTextPopup.Create(damageTaken,
                                transform.position,
                                true);
    }
}
