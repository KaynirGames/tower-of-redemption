using UnityEngine;

/// <summary>
/// Обновить наложенный эффект.
/// </summary>
[CreateAssetMenu(fileName = "NewUpdateEffectAM", menuName = "Scriptable Objects/Battle/Apply Methods/Update Effect")]
public class UpdateEffect : ApplyMethod
{
    public override void Handle(CharacterStats target, SkillEffect effect)
    {
        if (target.IsEffectExist(effect))
        {
            effect.Remove(target);
        }
    }
}
