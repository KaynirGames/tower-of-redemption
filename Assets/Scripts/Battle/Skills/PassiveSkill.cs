using System.Text;
using UnityEngine;

/// <summary>
/// Пассивное умение.
/// </summary>
[CreateAssetMenu(fileName = "NewPassiveSkill", menuName = "Scriptable Objects/Battle/Skill/Passive Skill")]
public class PassiveSkill : Skill
{
    public override void Activate(CharacterStats user, CharacterStats target)
    {
        _userEffects.ForEach(effect => effect.Apply(user));
        _enemyEffects.ForEach(effect => effect.Apply(target));
    }

    public override void Deactivate(CharacterStats user, CharacterStats target)
    {
        foreach (SkillEffect effect in _userEffects)
        {
            if (effect.DurationType.GetType() != typeof(PermanentDuration))
            {
                effect.Remove(target);
            }
        }

        foreach (SkillEffect effect in _enemyEffects)
        {
            if (effect.DurationType.GetType() != typeof(PermanentDuration))
            {
                effect.Remove(target);
            }
        }
    }

    public override StringBuilder GetParamsDescription()
    {
        throw new System.NotImplementedException();
    }
}
