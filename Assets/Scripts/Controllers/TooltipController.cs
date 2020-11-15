using UnityEngine;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using UnityEditor.Localization;

public class TooltipController : MonoBehaviour
{
    [SerializeField] private TooltipUI _tooltipUI = null;
    [SerializeField] private StringTableCollection _tooltipTableCollection = null;

    private void Awake()
    {
        TooltipHandlerUI.OnTooltipCall += ShowTooltip;
        TooltipHandlerUI.OnLinkedTooltipCall += ShowLinkedTooltip;
        TooltipHandlerUI.OnTooltipCancel += HideTooltip;
    }

    private void ShowTooltip(Vector3 position, string content, string header)
    {
        _tooltipUI.SetTooltipText(content, header);
        _tooltipUI.SetTooltipPosition(position);
        _tooltipUI.ToggleTooltip(true);
    }

    private void ShowLinkedTooltip(Vector3 position, string link, string header)
    {
        LocaleIdentifier localeID = LocalizationSettings.SelectedLocale.Identifier;
        StringTable table = (StringTable)_tooltipTableCollection.GetTable(localeID);

        ShowTooltip(position, table.GetEntry(link).GetLocalizedString(), header);
    }

    private void HideTooltip()
    {
        _tooltipUI.ToggleTooltip(false);
    }
}
