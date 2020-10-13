using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Undefined Passive", menuName = "Scriptable Objects/Battle/Skills/Passive Skill")]
public class PassiveSkill : Skill
{
    public override void Execute(Character owner, Character opponent, SkillInstance skillInstance)
    {
        if (owner != null)
        {
            _ownerEffects.ForEach(effect => effect.Apply(owner, skillInstance));
        }
        if (opponent != null)
        {
            _opponentEffects.ForEach(effect => effect.Apply(opponent, skillInstance));
        }
    }

    public override void Terminate(Character owner, Character opponent, SkillInstance skillInstance)
    {
        if (owner != null)
        {
            _ownerEffects.ForEach(effect => effect.Remove(owner, skillInstance));
        }
        if (opponent != null)
        {
            _opponentEffects.ForEach(effect => effect.Remove(opponent, skillInstance));
        }
    }

    public override string BuildDescription()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append(BuildEffectsDescription());

        builder.AppendLine(_flavorText.Value);

        return builder.ToString();
    }

    protected override void OnValidate()
    {
        base.OnValidate();

        _ownerEffects.RemoveAll(effect => effect.GetType() == typeof(StatBuff));
        _opponentEffects.RemoveAll(effect => effect.GetType() == typeof(StatBuff));
    }
}
