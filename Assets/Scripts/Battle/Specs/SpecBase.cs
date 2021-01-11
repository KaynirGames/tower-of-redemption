using UnityEditor;
using UnityEngine;
using UnityEngine.Localization;

public abstract class SpecBase : ScriptableObject, IIdentifiable
{
    [Header("Базовая информация о спеке:")]
    [SerializeField] private LocalizedString _specName = null;
    [SerializeField] private LocalizedString _specDescription = null;
    [Header("Базовые характеристики спека:")]
    [SerializeField] private float _baseHealth = 100;
    [SerializeField] private float _baseSpirit = 100;
    [SerializeField] private float _baseStrength = 0;
    [SerializeField] private float _baseWill = 0;
    [SerializeField] private float _baseDefence = 0;
    [SerializeField] private float _baseMagicDefence = 0;
    [Header("Эффективность стихий против спека:")]
    [SerializeField] private int _baseFireEfficacy = 100;
    [SerializeField] private int _baseAirEfficacy = 100;
    [SerializeField] private int _baseEarthEfficacy = 100;
    [SerializeField] private int _baseWaterEfficacy = 100;
    [Header("Базовые умения спека:")]
    [SerializeField] private SkillSO[] _baseSkills = null;

    public string SpecName => _specName.GetLocalizedString().Result;
    public string SpecDescription => _specDescription.GetLocalizedString().Result;

    public float BaseHealth => _baseHealth;
    public float BaseSpirit => _baseSpirit;
    public float BaseStrength => _baseStrength;
    public float BaseWill => _baseWill;
    public float BaseDefence => _baseDefence;
    public float BaseMagicDefence => _baseMagicDefence;

    public int BaseFireEfficacy => _baseFireEfficacy;
    public int BaseAirEfficacy => _baseAirEfficacy;
    public int BaseEarthEfficacy => _baseEarthEfficacy;
    public int BaseWaterEfficacy => _baseWaterEfficacy;

    public SkillSO[] BaseSkills => _baseSkills;

    public string ID { get; private set; }

#if UNITY_EDITOR
    protected void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        ID = AssetDatabase.AssetPathToGUID(path);
    }
#endif
}