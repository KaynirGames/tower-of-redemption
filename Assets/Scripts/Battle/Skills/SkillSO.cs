using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization;

public abstract class SkillSO : ScriptableObject, IIdentifiable
{
    [Header("Параметры отображения умения:")]
    [SerializeField] protected LocalizedString _name = null;
    [SerializeField] protected LocalizedString _description = null;
    [SerializeField] protected LocalizedString _type = null;
    [SerializeField] protected Sprite _icon = null;
    [SerializeField] protected SkillSlot _slot = SkillSlot.Active;
    [Header("Общие параметры умения:")]
    [SerializeField] protected int _cost = 0;
    [SerializeField] protected int _cooldown = 0;
    [SerializeField] protected List<EffectSO> _ownerEffects = new List<EffectSO>();
    [SerializeField] protected List<EffectSO> _opponentEffects = new List<EffectSO>();

    public string Name => _name.GetLocalizedString().Result;
    public string Description => _description.GetLocalizedString().Result;
    public string Type => _type.GetLocalizedString().Result;
    public Sprite Icon => _icon;
    public SkillSlot Slot => _slot;

    public int Cost => _cost;
    public int Cooldown => _cooldown;

    public string ID { get; private set; }

    public abstract void Execute(Character owner, Character opponent, Skill skill);

    public abstract void Terminate(Character owner, Character opponent, Skill skill);

    protected virtual void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        ID = AssetDatabase.AssetPathToGUID(path);
    }
}
