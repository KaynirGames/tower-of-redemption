using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EffectSlotUI : MonoBehaviour, ITooltipHandler
{
    [SerializeField] private Image _effectIcon = null;
    [SerializeField] private Image _durationIcon = null;
    [SerializeField] private TextMeshProUGUI _chargesText = null;

    private RectTransform _rectTransform;
    private EffectDisplayUI _effectDisplayUI;

    private Effect _effect;
    private float _effectDuration;

    private PoolManager _poolManager;
    private TooltipManager _tooltipManager;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _poolManager = PoolManager.Instance;
        _tooltipManager = TooltipManager.Instance;
    }

    public void RegisterSlot(EffectDisplayUI effectDisplayUI, Effect effect)
    {
        _effectDisplayUI = effectDisplayUI;
        _effect = effect;
        _effectDuration = effect.EffectSO.Duration;
        _effect.OnDurationTick += UpdateDurationDisplay;
        _effect.OnDurationExpire += StopDurationDisplay;

        if (effect.EffectSO.ChargesAmount > 0)
        {
            UpdateChargesDisplay(effect.EffectSO.ChargesAmount);
            _effect.OnChargeConsume += UpdateChargesDisplay;
            _chargesText.gameObject.SetActive(true);
        }

        _effectIcon.sprite = effect.EffectSO.EffectIcon;
        _durationIcon.fillAmount = 1;

        gameObject.SetActive(true);
    }

    public void Clear()
    {
        _effect.OnDurationExpire -= StopDurationDisplay;
        _effect.OnDurationTick -= UpdateDurationDisplay;

        if (_effect.EffectSO.ChargesAmount > 0)
        {
            _effect.OnChargeConsume -= UpdateChargesDisplay;
            _chargesText.gameObject.SetActive(false);
        }

        _tooltipManager.HideTooltip();
        _poolManager.Store(gameObject);
    }

    public void SetAsLastSlot()
    {
        _rectTransform.SetAsLastSibling();
    }

    public bool OnTooltipRequest(out string content, out string header)
    {
        if (_effect != null)
        {
            content = _effect.EffectSO.Tooltip;
            header = _effect.EffectSO.EffectName;
            return true;
        }
        else
        {
            content = null;
            header = null;
            return false;
        }
    }

    private void UpdateDurationDisplay(float timer)
    {
        _durationIcon.fillAmount = timer / _effectDuration;
    }

    private void UpdateChargesDisplay(int chargesLeft)
    {
        _chargesText.SetText(chargesLeft.ToString());
    }

    private void StopDurationDisplay()
    {
        _effectDisplayUI.HandleEmptySlot(this);

        Clear();
    }
}
