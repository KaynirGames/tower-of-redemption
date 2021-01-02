using KaynirGames.Tools;
using UnityEngine.EventSystems;

public class TooltipBattle : TooltipBase, IPointerEnterHandler, IPointerExitHandler
{
    private ITooltipHandler _tooltipHandler;

    private void Awake()
    {
        _tooltipHandler = GetComponent<ITooltipHandler>();
    }

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
        if (_tooltipHandler != null)
        {
            if (_tooltipHandler.OnTooltipRequest(out string content, out string header))
            {
                _tooltipManager.ShowTooltip(KaynirTools.GetPointerRawPosition(),
                                            content,
                                            header);
            }
        }
    }
}
