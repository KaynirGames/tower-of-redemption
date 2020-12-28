using KaynirGames.Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;

public class TooltipPreset : TooltipBase, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private LocalizedString _tooltipHeaderText = null;
    [SerializeField] private LocalizedString _tooltipContentText = null;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _lastShowRoutine = StartCoroutine(ShowTooltipRoutine());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Hide();
    }

    protected override void Show()
    {
        if (_tooltipContentText.IsEmpty) { return; }

        string content = _tooltipContentText.GetLocalizedString().Result;

        if (!string.IsNullOrEmpty(content))
        {
            string header = null;

            if (!_tooltipHeaderText.IsEmpty)
            {
                header = _tooltipHeaderText.GetLocalizedString().Result;
            }

            _tooltipManager.ShowTooltip(KaynirTools.GetPointerRawPosition(),
                                        content,
                                        header);
        }
    }
}
