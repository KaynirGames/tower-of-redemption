using KaynirGames.Tools;
using TMPro;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.EventSystems;

public class TooltipLink : TooltipBase, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private StringTableCollection _tooltipTable = null;
    [SerializeField] private TextMeshProUGUI _linkedTextField = null;

    public void OnPointerDown(PointerEventData eventData)
    {
        _lastShowRoutine = StartCoroutine(ShowTooltipRoutine());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Hide();
    }

    protected override void Show()
    {
        if (TryGetTooltip(out string content, out string header))
        {
            _tooltipManager.ShowTooltip(KaynirTools.GetPointerRawPosition(),
                                        content,
                                        header);
        }
    }

    private bool TryGetTooltip(out string content, out string header)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(_linkedTextField,
                                                       KaynirTools.GetPointerRawPosition(),
                                                       null);

        if (linkIndex >= 0)
        {
            TMP_LinkInfo linkInfo = _linkedTextField.textInfo.linkInfo[linkIndex];
            LocaleIdentifier localeID = LocalizationSettings.SelectedLocale.Identifier;
            StringTable table = (StringTable)_tooltipTable.GetTable(localeID);

            content = table.GetEntry(linkInfo.GetLinkID()).GetLocalizedString();
            header = linkInfo.GetLinkText();

            return true;
        }
        else
        {
            content = null;
            header = null;
            return false;
        }
    }
}
