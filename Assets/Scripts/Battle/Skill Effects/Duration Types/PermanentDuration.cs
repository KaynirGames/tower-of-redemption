using UnityEngine;

/// <summary>
/// Постоянная продолжительность эффекта (до конца игры).
/// </summary>
[CreateAssetMenu(fileName = "PermanentDuration", menuName = "Scriptable Objects/Battle/Duration Types/Permanent Duration")]
public class PermanentDuration : DurationType
{
    public override void Execute(SkillEffect effect, CharacterStats target)
    {
        target.PermanentEffects.Add(effect);
    }

    public override void Terminate(SkillEffect effect, CharacterStats target)
    {
        target.PermanentEffects.Add(effect);
    }
}
