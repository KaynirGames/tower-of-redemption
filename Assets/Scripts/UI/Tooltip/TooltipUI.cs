using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipUI : MonoBehaviour
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

        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
        _canvasGroup.alpha = 0;
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

    public void ToggleTooltip(bool enable)
    {
        _canvasGroup.alpha = enable ? 1 : 0;
    }

    public void SetTooltipPosition(Vector3 position)
    {
        position /= _canvasRect.localScale.x;

        if (position.x - _popupRect.rect.width <= 0)
        {
            position.x = _popupRect.rect.width;
        }

        if (position.y + _popupRect.rect.height >= _canvasRect.rect.height)
        {
            position.y = _canvasRect.rect.height - _popupRect.rect.height;
        }

        _popupRect.anchoredPosition = position;
    }

    private void ResizeTooltip()
    {
        int headerLength = _headerField.text.Length;
        int contentLength = _contentField.text.Length;

        _popupLayoutElement.enabled = (headerLength >= _textWrapLimit || contentLength >= _textWrapLimit)
            ? true
            : false;

        _canvasRect.RefreshLayoutGroups();
    }
}
