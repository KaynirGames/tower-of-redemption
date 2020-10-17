using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TooltipHandlerUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public delegate void OnTooltipPopupCall(Vector3 anchoredPosition, string tooltipKey);

    public static event OnTooltipPopupCall OnTooltipCall = delegate { };
    public static event Action OnTooltipCancel = delegate { };

    [SerializeField] private TextMeshProUGUI _textMeshPro = null;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_textMeshPro.text != string.Empty)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(_textMeshPro, Input.mousePosition, null);

            if (linkIndex >= 0)
            {
                TMP_LinkInfo linkInfo = _textMeshPro.textInfo.linkInfo[linkIndex];
                OnTooltipCall.Invoke(Input.mousePosition, linkInfo.GetLinkID());
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnTooltipCancel.Invoke();
    }
}
