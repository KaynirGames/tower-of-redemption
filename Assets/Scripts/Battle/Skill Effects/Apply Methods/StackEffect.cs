using UnityEngine;

/// <summary>
/// Суммировать наложенный эффект.
/// </summary>
[CreateAssetMenu(fileName = "NewStackEffectAM", menuName = "Scriptable Objects/Battle/Apply Methods/Stack Effect")]
public class StackEffect : ApplyMethod
{
    public override void Handle(CharacterStats target, SkillEffect effect) { }
}
