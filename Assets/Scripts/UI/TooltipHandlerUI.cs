using KaynirGames.Tools;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipHandlerUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public delegate void OnTooltipPopupCall(Vector3 position, string content, string header);

    public static event OnTooltipPopupCall OnTooltipCall = delegate { };
    public static event OnTooltipPopupCall OnLinkedTooltipCall = delegate { };
    public static event Action OnTooltipCancel = delegate { };

    [SerializeField] private TextMeshProUGUI _linkedTextField = null;
    [SerializeField] private TranslatedText _tooltipHeaderText = null;
    [SerializeField] private TranslatedText _tooltipContentText = null;
    [SerializeField] private float _tooltipDelay = 0f;
    [SerializeField] private TooltipType _tooltipType = TooltipType.Normal;

    private WaitForSecondsRealtime _waitForTooltipCall;
    private Coroutine _lastCallRoutine;

    private void Awake()
    {
        _waitForTooltipCall = new WaitForSecondsRealtime(_tooltipDelay);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _lastCallRoutine = StartCoroutine(TooltipCallRoutine());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_lastCallRoutine != null)
        {
            StopCoroutine(_lastCallRoutine);
        }

        OnTooltipCancel.Invoke();
    }

    private void HandleTooltipCall()
    {
        switch (_tooltipType)
        {
            case TooltipType.Normal:
                CallTooltip();
                break;
            case TooltipType.Linked:
                CallLinkedTooltip();
                break;
        }
    }

    private void CallTooltip()
    {
        string header = _tooltipHeaderText.Value;
        string content = _tooltipContentText.Value;

        if (!string.IsNullOrEmpty(content))
        {
            OnTooltipCall.Invoke(KaynirTools.GetPointerRawPosition(),
                                 content,
                                 header);
        }
    }

    private void CallLinkedTooltip()
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(_linkedTextField,
                                                               KaynirTools.GetPointerRawPosition(),
                                                               null);

        if (linkIndex >= 0)
        {
            TMP_LinkInfo linkInfo = _linkedTextField.textInfo.linkInfo[linkIndex];

            OnLinkedTooltipCall.Invoke(KaynirTools.GetPointerRawPosition(),
                                       linkInfo.GetLinkID(),
                                       linkInfo.GetLinkText());
        }
    }

    private IEnumerator TooltipCallRoutine()
    {
        yield return _waitForTooltipCall;

        HandleTooltipCall();
    }

    private enum TooltipType
    {
        Normal,
        Linked
    }
}
