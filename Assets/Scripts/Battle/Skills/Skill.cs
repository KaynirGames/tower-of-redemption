using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    [Header("Параметры отображения умения:")]
    [SerializeField] protected TranslatedText _name = new TranslatedText("Skill.TextID.Name");
    [SerializeField] protected TranslatedText _flavorText = new TranslatedText("Skill.TextID.Flavor");
    [SerializeField] protected TranslatedText _description = new TranslatedText("Skill.TextID.Description");
    [SerializeField] protected SkillData _skillData = null;
    [SerializeField] protected Sprite _icon = null;
    [SerializeField] protected SkillSlot _slot = SkillSlot.Active;
    [Header("Общие параметры умения:")]
    [SerializeField] protected int _cost = 0;
    [SerializeField] protected int _cooldown = 0;
    [SerializeField] protected List<Effect> _ownerEffects = new List<Effect>();
    [SerializeField] protected List<Effect> _opponentEffects = new List<Effect>();

    public string Name => _name.Value;
    public string Description => _description.Value;
    public string TypeName => _skillData.TypeName;
    public Sprite Icon => _icon;
    public SkillSlot Slot => _slot;

    public int Cost => _cost;
    public int Cooldown => _cooldown;

    public abstract void Execute(Character owner, Character opponent, SkillInstance skillInstance);

    public abstract void Terminate(Character owner, Character opponent, SkillInstance skillInstance);

    public abstract string BuildDescription();

    protected virtual void OnValidate()
    {

    }

    protected string BuildEffectsDescription()
    {
        StringBuilder builder = new StringBuilder();

        if (_ownerEffects.Count > 0 || _opponentEffects.Count > 0)
        { 
            builder.AppendLine();
        }

        _ownerEffects.ForEach(eff => builder.AppendLine(eff.GetDescription()));
        _opponentEffects.ForEach(eff => builder.AppendLine(eff.GetDescription()));

        return builder.ToString();
    }
}
