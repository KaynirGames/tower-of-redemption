using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// Пассивное умение.
/// </summary>
[CreateAssetMenu(fileName = "NewPassiveSkill", menuName = "Scriptable Objects/Battle/Skills/Passive Skill")]
public class PassiveSkill : Skill
{
    public override void Activate(Character owner, Character opponent)
    {
        ApplyPassiveEffects(owner, _ownerEffects);
        ApplyPassiveEffects(opponent, _opponentEffects);
    }

    public override void Deactivate(Character owner, Character opponent)
    {
        RemovePassiveEffects(owner, _ownerEffects);
        RemovePassiveEffects(opponent, _opponentEffects);
    }

    public override StringBuilder GetParamsDescription()
    {
        _stringBuilder.Clear();

        _ownerEffects.ForEach(effect => _stringBuilder.AppendLine(effect.GetDescription(TargetType.Self)));
        _opponentEffects.ForEach(effect => _stringBuilder.AppendLine(effect.GetDescription(TargetType.Opponent)));

        return _stringBuilder;
    }

    private void ApplyPassiveEffects(Character character, List<SkillEffect> effects)
    {
        if (character != null)
        {
            effects.ForEach(effect => effect.Apply(character.Stats));
        }
    }

    private void RemovePassiveEffects(Character character, List<SkillEffect> effects)
    {
        if (character != null)
        {
            foreach (SkillEffect effect in effects)
            {
                if (effect.DurationType.GetType() != typeof(PermanentDuration))
                {
                    effect.Remove(character.Stats);
                }
            }
        }
    }
}
