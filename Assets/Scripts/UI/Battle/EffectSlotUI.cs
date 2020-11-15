using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EffectSlotUI : MonoBehaviour
{
    [SerializeField] private Image _effectIcon = null;
    [SerializeField] private Image _durationIcon = null;
    [SerializeField] private TextMeshProUGUI _chargesText = null;

    private RectTransform _rectTransform;
    private EffectDisplayUI _effectDisplayUI;

    private Effect _effectInstance;
    private float _effectDuration;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _effectDisplayUI = gameObject.GetComponentInParent<EffectDisplayUI>();
    }

    public void RegisterEffect(Effect effectInstance)
    {
        _effectInstance = effectInstance;
        _effectDuration = effectInstance.EffectSO.Duration;
        _effectInstance.OnDurationTick += UpdateDurationDisplay;
        _effectInstance.OnDurationExpire += StopDurationDisplay;

        if (effectInstance.EffectSO.ChargesAmount > 0)
        {
            UpdateChargesDisplay(effectInstance.EffectSO.ChargesAmount);
            _effectInstance.OnChargeConsume += UpdateChargesDisplay;
            _chargesText.gameObject.SetActive(true);
        }

        _effectIcon.sprite = effectInstance.EffectSO.EffectIcon;
        _durationIcon.sprite = effectInstance.EffectSO.EffectIcon;
        _durationIcon.fillAmount = 0;

        gameObject.SetActive(true);
    }

    public void Clear()
    {
        _effectInstance.OnDurationTick -= UpdateDurationDisplay;
        _effectInstance.OnDurationExpire -= StopDurationDisplay;

        if (_effectInstance.EffectSO.ChargesAmount > 0)
        {
            _effectInstance.OnChargeConsume -= UpdateChargesDisplay;
            _chargesText.gameObject.SetActive(false);
        }

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
        _durationIcon.fillAmount = 1 - timer / _effectDuration;
    }

    private void UpdateChargesDisplay(int chargesLeft)
    {
        _chargesText.SetText(chargesLeft.ToString());
    }

    private void StopDurationDisplay()
    {
        Clear();
        _effectDisplayUI.HandleEmptySlot(this);
    }
}
