using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private PlayerUI _playerUI = null;
    [SerializeField] private DescriptionUI _descriptionUI = null;

    private CanvasGroup _menuCanvasGroup;
    private ActionPopupUI _actionPopupUI;

    private void Awake()
    {
        _menuCanvasGroup = GetComponent<CanvasGroup>();

        ActionPopupUI.OnItemDescriptionCall += ShowDescription;
        SkillSlotUI.OnSkillDescriptionCall += ShowDescription;
        SelectionHandlerUI.OnSelectionCancel += CancelSelection;
    }

    private void Start()
    {
        _actionPopupUI = AssetManager.Instance.ActionPopup;
    }

    public void OpenMenu()
    {
        _playerUI.TogglePlayerHUD(false);

        ToggleMenuWindow(true);

        GameMaster.Instance.TogglePause(true);
    }

    public void CloseMenu()
    {
        ToggleMenuWindow(false);
        CancelSelection();

        _playerUI.TogglePlayerHUD(true);

        GameMaster.Instance.TogglePause(false);
    }

    public void ToggleMenuWindow(bool enable)
    {
        _menuCanvasGroup.alpha = enable ? 1 : 0;
        _menuCanvasGroup.blocksRaycasts = enable;
    }

    public void CancelSelection()
    {
        _descriptionUI.ClearDescriptionText();
        _actionPopupUI.Toggle(false);
    }

    private void ShowDescription(string name, string type, string description)
    {
        _descriptionUI.SetDescriptionText(name, type, description);
    }
}