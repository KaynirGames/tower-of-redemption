using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance { get; private set; }

    [SerializeField] private TooltipUI _tooltipUI = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ShowTooltip(Vector3 position, string content, string header)
    {
        _tooltipUI.SetTooltipText(content, header);
        _tooltipUI.SetTooltipPosition(position);
        _tooltipUI.ToggleTooltip(true);
    }

    public void HideTooltip()
    {
        _tooltipUI.ToggleTooltip(false);
    }
}
