using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using KaynirGames.Tools;
using System.Collections;

public class TooltipHandlerUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public delegate void OnTooltipPopupCall(Vector3 position, string content, string header);

    public static event OnTooltipPopupCall OnTooltipCall = delegate { };
    public static event Action OnTooltipCancel = delegate { };

    [SerializeField] private TextMeshProUGUI _linkedTextField = null;
    [SerializeField] private TranslatedText _tooltipHeaderText = null;
    [SerializeField] private TranslatedText _tooltipContentText = null;
    [SerializeField] private float _tooltipDelay = 0f;

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
        string header = string.Empty;
        string content = string.Empty;

        if (_linkedTextField != null)
        {
            content = GetTextByLink();
        }
        else
        {
            if (_tooltipHeaderText != null) { header = _tooltipHeaderText.Value; }
            if (_tooltipContentText != null) { content = _tooltipContentText.Value; }
        }

        if (content == string.Empty) { return; }

        OnTooltipCall.Invoke(KaynirTools.GetPointerRawPosition(), content, header);
    }

    private string GetTextByLink()
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(_linkedTextField, Input.mousePosition, null);

        if (linkIndex >= 0)
        {
            TMP_LinkInfo linkInfo = _linkedTextField.textInfo.linkInfo[linkIndex];

            return Translator.GetTranslationLine(linkInfo.GetLinkID());
        }

        return string.Empty;
    }

    private IEnumerator TooltipCallRoutine()
    {
        yield return _waitForTooltipCall;

        HandleTooltipCall();
    }
}
