using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Продолжительность эффекта до конца боя.
/// </summary>
[CreateAssetMenu(fileName = "BattleDuration", menuName = "Scriptable Objects/Battle/Duration Types/Battle Duration")]
public class BattleDuration : DurationType
{
    /// <summary>
    /// Делегат для сообщения о завершении эффектов, действующих до конца боя.
    /// </summary>
    public delegate void OnBattleDurationExpire(CharacterStats target);

    public override void Execute(SkillEffect effect, CharacterStats target)
    {
        target.InflictedEffects.Add(effect);
        BattleManager.Instance.OnBattleDurationExpire += effect.Remove;
    }

    public override void Terminate(SkillEffect effect, CharacterStats target)
    {
        target.InflictedEffects.Remove(effect);
        BattleManager.Instance.OnBattleDurationExpire -= effect.Remove;
    }
}
