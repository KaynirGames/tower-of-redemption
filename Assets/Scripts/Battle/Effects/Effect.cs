using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Effect
{
    public delegate void OnDurationTimerTick(float durationTimer);
    public delegate void OnEffectChargeConsume(int chargesLeft);

    public event OnDurationTimerTick OnDurationTick = delegate { };
    public event OnEffectChargeConsume OnChargeConsume = delegate { };
    public event Action OnDurationExpire = delegate { };

    public EffectSO EffectSO { get; private set; }
    public object EffectSource { get; private set; }

    private Character _target;

    private int _durationTimer;
    private int _secondsAmountOverTick;
    private int _chargesAmount = -1;

    private WaitForSeconds _waitForNextTick;
    private Coroutine _lastDurationRoutine;

    public Effect(EffectSO effectSO, Character target, object effectSource)
    {
        EffectSO = effectSO;
        EffectSource = effectSource;
        _target = target;

        if (effectSO.ChargesAmount > 0)
        {
            _chargesAmount = effectSO.ChargesAmount;
        }

        if (effectSO.Duration > 0)
        {
            _durationTimer = effectSO.Duration;
            _secondsAmountOverTick = effectSO.SecondsAmountOverTick;
            _waitForNextTick = new WaitForSeconds(_secondsAmountOverTick);
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

        EffectSO.Remove(_target, this);
        OnDurationExpire.Invoke();
    }

    public void ResetDuration()
    {
        _durationTimer += EffectSO.Duration;
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
            EffectSO.Tick(_target);

            _durationTimer -= _secondsAmountOverTick;
            OnDurationTick.Invoke(_durationTimer);

            yield return _waitForNextTick;
        }

        RemoveEffect();
    }
}