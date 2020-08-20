using System.Collections;
using UnityEngine;

/// <summary>
/// Продолжительность эффекта согласно таймеру.
/// </summary>
[CreateAssetMenu(fileName = "Time_TimerDuration", menuName = "Scriptable Objects/Battle/Duration Types/Timer Duration")]
public class TimerDuration : DurationType
{
    [SerializeField] private float _timer = 0f; // Таймер продолжительности эффекта.

    public override string TypeName => string.Format("{0} {1}", _timer, _typeNameText.Value);

    public override void Execute(SkillEffect effect, CharacterStats target)
    {
        target.InflictedEffects.Add(effect);
        target.StartCoroutine(TimerRoutine(effect, target, _timer));
    }

    public override void Terminate(SkillEffect effect, CharacterStats target)
    {
        target.InflictedEffects.Remove(effect);
    }
    /// <summary>
    /// Корутина таймера до снятия эффекта.
    /// </summary>
    private IEnumerator TimerRoutine(SkillEffect effect, CharacterStats target, float timer)
    {
        yield return new WaitForSeconds(timer);
        effect.Remove(target);
    }
}
