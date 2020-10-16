using TMPro;
using UnityEngine;

public class TooltipPopupUI : MonoBehaviour
{
    [SerializeField] private RectTransform _popupRect = null;
    [SerializeField] private RectTransform _backgroundRect = null;
    [SerializeField] private TextMeshProUGUI _tooltipText = null;
    [SerializeField] private Vector2 _textPadding = new Vector2(40, 40);

    private CanvasGroup _canvasGroup;
    private RectTransform _canvasRect;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasRect = GetComponent<RectTransform>();
    }

    public void SetTooltipText(string tooltipText)
    {
        _tooltipText.SetText(tooltipText);
        _tooltipText.ForceMeshUpdate();

        Vector2 textSize = _tooltipText.GetRenderedValues(false);

        _backgroundRect.sizeDelta = textSize + _textPadding;
    }

    public void ToggleTooltipPopup(bool enable)
    {
        _canvasGroup.alpha = enable ? 1 : 0;
    }

    public void SetTooltipPosition(Vector3 anchoredPosition)
    {
        anchoredPosition /= _canvasRect.localScale.x;

        if (anchoredPosition.x - _backgroundRect.rect.width < 0)
        {
            anchoredPosition.x = _backgroundRect.rect.width;
        }

        if (anchoredPosition.y + _backgroundRect.rect.height > _canvasRect.rect.height)
        {
            anchoredPosition.y = _canvasRect.rect.height - _backgroundRect.rect.height;
        }

        _popupRect.anchoredPosition = anchoredPosition;
    }
}
