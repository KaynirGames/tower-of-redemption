using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Localization;

public abstract class SpecBase : ScriptableObject
{
    [Header("Базовая информация о персонаже:")]
    [SerializeField] private LocalizedString _specName = null;
    [SerializeField] private LocalizedString _specDescription = null;
    [Header("Базовые статы персонажа:")]
    [SerializeField] private float _baseHealth = 100;
    [SerializeField] private float _baseEnergy = 100;
    [SerializeField] private float _baseStrength = 0;
    [SerializeField] private float _baseWill = 0;
    [SerializeField] private float _baseDefence = 0;
    [SerializeField] private float _baseMagicDefence = 0;
    [Header("Эффективность стихий против персонажа:")]
    [SerializeField] private int _baseFireEfficacy = 100;
    [SerializeField] private int _baseAirEfficacy = 100;
    [SerializeField] private int _baseEarthEfficacy = 100;
    [SerializeField] private int _baseWaterEfficacy = 100;
    [Header("Базовые умения персонажа:")]
    [SerializeField] private List<SkillSO> _baseSkills = new List<SkillSO>();

    public string SpecName => _specName.GetLocalizedString().Result;
    public string SpecDescription => _specDescription.GetLocalizedString().Result;

    public float BaseHealth => _baseHealth;
    public float BaseEnergy => _baseEnergy;
    public float BaseStrength => _baseStrength;
    public float BaseWill => _baseWill;
    public float BaseDefence => _baseDefence;
    public float BaseMagicDefence => _baseMagicDefence;

    public int BaseFireEfficacy => _baseFireEfficacy;
    public int BaseAirEfficacy => _baseAirEfficacy;
    public int BaseEarthEfficacy => _baseEarthEfficacy;
    public int BaseWaterEfficacy => _baseWaterEfficacy;

    public ReadOnlyCollection<SkillSO> BaseSkills => _baseSkills.AsReadOnly();
}
