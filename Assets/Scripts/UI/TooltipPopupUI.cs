using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipPopupUI : MonoBehaviour
{
    [SerializeField] private RectTransform _popupRect = null;
    [SerializeField] private LayoutElement _popupLayoutElement = null;
    [SerializeField] private TextMeshProUGUI _headerField = null;
    [SerializeField] private TextMeshProUGUI _contentField = null;
    [SerializeField] private int _textWrapLimit = 200;

    private CanvasGroup _canvasGroup;
    private RectTransform _canvasRect;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasRect = GetComponent<RectTransform>();
    }

    public void SetTooltipText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            _headerField.gameObject.SetActive(false);
        }
        else
        {
            _headerField.SetText(header);
            _headerField.gameObject.SetActive(true);
        }

        _contentField.SetText(content);

        ResizeTooltip();
    }

    public void ToggleTooltipPopup(bool enable)
    {
        _canvasGroup.alpha = enable ? 1 : 0;
    }

    public void SetTooltipPosition(Vector3 anchoredPosition)
    {
        anchoredPosition /= _canvasRect.localScale.x;

        if (anchoredPosition.x - _popupRect.rect.width < 0)
        {
            anchoredPosition.x = _popupRect.rect.width;
        }

        if (anchoredPosition.y + _popupRect.rect.height > _canvasRect.rect.height)
        {
            anchoredPosition.y = _canvasRect.rect.height - _popupRect.rect.height;
        }

        _popupRect.anchoredPosition = anchoredPosition;
    }

    private void ResizeTooltip()
    {
        int headerLength = _headerField.text.Length;
        int contentLength = _contentField.text.Length;

        _popupLayoutElement.enabled = (headerLength > _textWrapLimit || contentLength > _textWrapLimit)
            ? true
            : false;
    }
}
