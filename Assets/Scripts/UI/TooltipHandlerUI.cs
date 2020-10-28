using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using KaynirGames.Tools;

public class TooltipHandlerUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public delegate void OnTooltipPopupCall(Vector3 anchoredPosition, string tooltipKey);

    public static event OnTooltipPopupCall OnTooltipCall = delegate { };
    public static event Action OnTooltipCancel = delegate { };

    [SerializeField] private TooltipCallType _tooltipCallType = TooltipCallType.ByLink;
    [SerializeField] private TextMeshProUGUI _textWithLinks = null;
    [SerializeField] private TranslatedText _tooltipText = null;

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (_tooltipCallType)
        {
            case TooltipCallType.ByLink:
                HandleCallByLink();
                break;
            case TooltipCallType.ByKey:
                HandleCallByKey();
                break;
        }
    }

    private void HandleCallByLink()
    {
        if (_textWithLinks.text != string.Empty)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(_textWithLinks, Input.mousePosition, null);

            if (linkIndex >= 0)
            {
                TMP_LinkInfo linkInfo = _textWithLinks.textInfo.linkInfo[linkIndex];
                OnTooltipCall.Invoke(KaynirTools.GetPointerRawPosition(), linkInfo.GetLinkID());
            }
        }
    }

    private void HandleCallByKey()
    {
        if (_tooltipText != null)
        {
            OnTooltipCall.Invoke(KaynirTools.GetPointerRawPosition(), _tooltipText.Key);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnTooltipCancel.Invoke();
    }

    private enum TooltipCallType
    {
        ByLink,
        ByKey
    }
}
