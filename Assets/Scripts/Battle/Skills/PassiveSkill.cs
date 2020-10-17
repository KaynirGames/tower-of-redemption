using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Undefined Passive", menuName = "Scriptable Objects/Battle/Skills/Passive Skill")]
public class PassiveSkill : Skill
{
    [Header("Параметры пассивного умения:")]
    [SerializeField] private List<StatBonus> _ownerBonuses = new List<StatBonus>();

    public override void Execute(Character owner, Character opponent, SkillInstance skillInstance)
    {
        _ownerEffects.ForEach(effect => effect.Apply(owner, skillInstance));
        _opponentEffects.ForEach(effect => effect.Apply(opponent, skillInstance));
    }

    public override void Terminate(Character owner, Character opponent, SkillInstance skillInstance)
    {
        _ownerEffects.ForEach(effect => effect.Remove(owner, skillInstance));
        _opponentEffects.ForEach(effect => effect.Remove(opponent, skillInstance));
    }

    public override string BuildDescription()
    {
        StringBuilder builder = new StringBuilder();

        foreach (StatBonus bonus in _ownerBonuses)
        {
            builder.AppendLine(bonus.GetDescription(_skillType.TargetOwner));
        }

        builder.AppendLine();

        builder.Append(BuildEffectsDescription());

        builder.AppendLine(_flavorText.Value);

        return builder.ToString();
    }

    public void ApplyPermanentEffects(Character owner, SkillInstance skillInstance)
    {
        _ownerBonuses.ForEach(bonus => bonus.Apply(owner, skillInstance));
    }

    public void RemovePermanentEffects(Character owner, SkillInstance skillInstance)
    {
        _ownerBonuses.ForEach(bonus => bonus.Remove(owner, skillInstance));
    }

    protected override void OnValidate()
    {
        base.OnValidate();

        _ownerEffects.RemoveAll(effect => effect.GetType() == typeof(StatBuff));
        _opponentEffects.RemoveAll(effect => effect.GetType() == typeof(StatBuff));

        _ownerEffects.RemoveAll(effect => effect.GetType() == typeof(StatBonus));
        _opponentEffects.RemoveAll(effect => effect.GetType() == typeof(StatBonus));
    }
}
