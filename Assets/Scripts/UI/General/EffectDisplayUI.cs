using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectDisplayUI : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect = null;
    [SerializeField] private RectTransform _effectSlotsParent = null;
    [SerializeField] private EffectSlotUI _effectSlotPrefab = null;
    [Header("Параметры отображения эффектов:")]
    [SerializeField] private float _scrollSpeed = 0.5f;
    [SerializeField] private int _minAmountToScroll = 4;
    [SerializeField, Range(0, 1)] private float _scrollResetLoopPosition = 0.25f;

    private CharacterEffects _characterEffects;
    private bool _enableScrolling;

    private List<EffectSlotUI> _effectSlots;
    private Queue<EffectSlotUI> _emptySlotsPool;

    private Coroutine _lastScrollRoutine = null;

    private void Awake()
    {
        _effectSlots = new List<EffectSlotUI>();
        _emptySlotsPool = new Queue<EffectSlotUI>();
    }

    public void RegisterCharacterEffects(CharacterEffects characterEffects)
    {
        _characterEffects = characterEffects;
        _characterEffects.OnDisplayEffectCall += UpdateEffectsDisplay;
    }

    public void HandleEmptySlot(EffectSlotUI effectSlot)
    {
        _effectSlots.Remove(effectSlot);
        _emptySlotsPool.Enqueue(effectSlot);

        HandleContentScroll();
    }

    public void ClearEffectsUI()
    {
        _effectSlots.ForEach(slot => slot.Clear());
    }

    private void UpdateEffectsDisplay(Effect instance)
    {
        _effectSlots.Add(CreateSlot(instance));

        HandleContentScroll();
    }

    private EffectSlotUI CreateSlot(Effect instance)
    {
        EffectSlotUI effectSlot = _emptySlotsPool.Count > 0
            ? GetSlotFromPool()
            : Instantiate(_effectSlotPrefab, _effectSlotsParent);

        effectSlot.RegisterEffect(instance);

        return effectSlot;
    }

    private EffectSlotUI GetSlotFromPool()
    {
        EffectSlotUI slot = _emptySlotsPool.Dequeue();
        slot.SetSlotParent(_effectSlotsParent);

        return slot;
    }

    private void HandleContentScroll()
    {
        _enableScrolling = _effectSlots.Count >= _minAmountToScroll
            ? true
            : false;

        if (_enableScrolling)
        {
            if (_lastScrollRoutine == null)
            {
                _lastScrollRoutine = StartCoroutine(ScrollContentRoutine());
            }
        }
        else
        {
            if (_lastScrollRoutine != null)
            {
                StopCoroutine(_lastScrollRoutine);
                _scrollRect.verticalNormalizedPosition = 1;
                _lastScrollRoutine = null;
            }
        }
    }

    private IEnumerator ScrollContentRoutine()
    {
        _scrollRect.verticalNormalizedPosition = 1;

        while (_enableScrolling)
        {
            _scrollRect.verticalNormalizedPosition -= _scrollSpeed * Time.deltaTime;

            if (_scrollRect.verticalNormalizedPosition <= _scrollResetLoopPosition)
            {
                var slot = _effectSlots[0];

                _effectSlots.RemoveAt(0);
                _effectSlots.Add(slot);

                slot.SetAsLastSlot();

                _scrollRect.verticalNormalizedPosition = 1;
            }

            yield return null;
        }
    }
}
