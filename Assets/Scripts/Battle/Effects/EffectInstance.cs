using System.Collections;
using UnityEngine;

/// <summary>
/// Экземпляр эффекта, накладываемого на персонажа.
/// </summary>
[System.Serializable]
public class EffectInstance
{
    public Effect Effect { get; private set; }
    public object EffectSource { get; private set; }

    private Character _target;

    private int _durationTimer;
    private int _secondsAmountOverTick;
    private WaitForSecondsRealtime _waitForNextTick;

    public EffectInstance(Effect effect, Character target, object effectSource)
    {
        Effect = effect;
        EffectSource = effectSource;
        _target = target;

        if (effect.Duration > 0)
        {
            _durationTimer = effect.Duration;
            _secondsAmountOverTick = effect.SecondsAmountOverTick;
            _waitForNextTick = new WaitForSecondsRealtime(_secondsAmountOverTick);
        }
    }

    public void StartDuration()
    {
        if (_durationTimer > 0)
        {
            _target.StartCoroutine(DurationRoutine());
        }
    }

    public void RemoveEffect()
    {
        Effect.Remove(_target, this);
    }

    public void ResetDuration()
    {
        _durationTimer = Effect.Duration;
    }

    private IEnumerator DurationRoutine()
    {
        while (_durationTimer >= 0)
        {
            Effect.Tick(_target);

            _durationTimer -= _secondsAmountOverTick;

            yield return _waitForNextTick;
        }

        RemoveEffect();
    }
}