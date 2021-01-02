using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectDisplayUI : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect = null;
    [SerializeField] private RectTransform _effectSlotsParent = null;
    [Header("Параметры отображения эффектов:")]
    [SerializeField] private float _scrollSpeed = 0.5f;
    [SerializeField] private int _minAmountToScroll = 4;
    [SerializeField, Range(0, 1)] private float _scrollResetLoopPosition = 0.25f;

    private CharacterEffects _characterEffects;
    private bool _enableScrolling;

    private List<EffectSlotUI> _effectSlots;
    private PoolManager _poolManager;

    private Coroutine _lastScrollRoutine = null;

    private void Awake()
    {
        _effectSlots = new List<EffectSlotUI>();
    }

    private void Start()
    {
        _poolManager = PoolManager.Instance;
    }

    public void RegisterCharacterEffects(CharacterEffects characterEffects)
    {
        _characterEffects = characterEffects;
        _characterEffects.OnDisplayEffectCall += UpdateEffectsDisplay;
    }

    public void HandleEmptySlot(EffectSlotUI effectSlot)
    {
        _effectSlots.Remove(effectSlot);
        HandleContentScroll();
    }

    public void ClearEffectsUI()
    {
        _effectSlots.ForEach(slot => slot.Clear());
        _effectSlots.Clear();

        _characterEffects.OnDisplayEffectCall -= UpdateEffectsDisplay;
        _characterEffects = null;
    }

    private void UpdateEffectsDisplay(Effect effect)
    {
        _effectSlots.Add(CreateSlot(effect));
        HandleContentScroll();
    }

    private EffectSlotUI CreateSlot(Effect effect)
    {
        GameObject slotObject = _poolManager.Take(PoolTags.EffectSlot.ToString());
        slotObject.transform.SetParent(_effectSlotsParent, false);

        EffectSlotUI effectSlot = slotObject.GetComponent<EffectSlotUI>();
        effectSlot.RegisterSlot(this, effect);

        return effectSlot;
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
