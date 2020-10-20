﻿using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Экземпляр эффекта, накладываемого на персонажа.
/// </summary>
[Serializable]
public class EffectInstance
{
    public delegate void OnDurationTimerTick(float durationTimer);
    public delegate void OnEffectChargeConsume(int chargesLeft);

    public event OnDurationTimerTick OnDurationTick = delegate { };
    public event OnEffectChargeConsume OnChargeConsume = delegate { };
    public event Action OnDurationExpire = delegate { };

    public Effect Effect { get; private set; }
    public object EffectSource { get; private set; }

    private Character _target;

    private int _durationTimer;
    private int _secondsAmountOverTick;
    private int _chargesAmount = -1;

    private WaitForSecondsRealtime _waitForNextTick;
    private Coroutine _lastDurationRoutine;

    public EffectInstance(Effect effect, Character target, object effectSource)
    {
        Effect = effect;
        EffectSource = effectSource;
        _target = target;

        if (effect.ChargesAmount > 0)
        {
            _chargesAmount = effect.ChargesAmount;
        }

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
            if (!_target.gameObject.activeSelf) { return; }

            _lastDurationRoutine = _target.StartCoroutine(DurationRoutine());
            _target.Effects.DisplayEffect(this);
        }
    }

    public void RemoveEffect()
    {
        if (_lastDurationRoutine != null)
        {
            _target.StopCoroutine(_lastDurationRoutine);
        }

        Effect.Remove(_target, this);
        OnDurationExpire.Invoke();
    }

    public void ResetDuration()
    {
        _durationTimer = Effect.Duration;
    }

    public void RemoveCharge()
    {
        if (_chargesAmount > 0)
        {
            _chargesAmount--;

            OnChargeConsume.Invoke(_chargesAmount);

            if (_chargesAmount == 0)
            {
                RemoveEffect();
            }
        }
    }

    private IEnumerator DurationRoutine()
    {
        while (_durationTimer > 0)
        {
            Effect.Tick(_target);

            _durationTimer -= _secondsAmountOverTick;
            OnDurationTick.Invoke(_durationTimer);

            yield return _waitForNextTick;
        }

        _lastDurationRoutine = null;
        RemoveEffect();
    }
}