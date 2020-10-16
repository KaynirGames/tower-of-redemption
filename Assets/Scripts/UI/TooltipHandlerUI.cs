using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TooltipHandlerUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public delegate void OnTooltipPopupCall(Vector3 anchoredPosition, string tooltipTextID);

    public static event OnTooltipPopupCall OnTooltipCall = delegate { };
    public static event Action OnTooltipCancel = delegate { };

    [SerializeField] private TextMeshProUGUI _textMeshPro = null;

    public void OnPointerDown(PointerEventData eventData)
    {
        // Get TMPRo link info, if link is not null, then make tooltip call

        OnTooltipCall.Invoke(Input.mousePosition, "");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnTooltipCancel.Invoke();
    }
}
