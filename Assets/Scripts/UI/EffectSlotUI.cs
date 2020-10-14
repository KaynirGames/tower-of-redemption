using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectSlotUI : MonoBehaviour
{
    [SerializeField] private Image _effectIcon = null;
    [SerializeField] private Image _durationIcon = null;

    private RectTransform _rectTransform;
    private EffectDisplayUI _effectDisplayUI;

    private EffectInstance _effectInstance;
    private float _effectDuration;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _effectDisplayUI = gameObject.GetComponentInParent<EffectDisplayUI>();
    }

    public void RegisterEffect(EffectInstance effectInstance)
    {
        _effectInstance = effectInstance;
        _effectDuration = effectInstance.Effect.Duration;
        _effectInstance.OnDurationTick += UpdateDurationDisplay;
        _effectInstance.OnDurationExpire += StopDurationDisplay;

        _effectIcon.sprite = effectInstance.Effect.EffectIcon;
        _durationIcon.fillAmount = 1;

        gameObject.SetActive(true);
    }

    public void Clear()
    {
        _effectInstance.OnDurationTick -= UpdateDurationDisplay;
        _effectInstance.OnDurationExpire -= StopDurationDisplay;
        _effectInstance = null;

        SetSlotParent(null);
        gameObject.SetActive(false);
    }

    public void SetAsLastSlot()
    {
        _rectTransform.SetAsLastSibling();
    }

    public void SetSlotParent(Transform parent)
    {
        _rectTransform.SetParent(parent);
    }

    private void UpdateDurationDisplay(float timer)
    {
        _durationIcon.fillAmount = timer / _effectDuration;
    }

    private void StopDurationDisplay()
    {
        Clear();
        _effectDisplayUI.HandleEmptySlot(this);
    }
}
