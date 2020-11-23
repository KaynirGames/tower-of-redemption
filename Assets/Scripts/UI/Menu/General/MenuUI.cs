using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public static MenuUI Instance { get; private set; }

    [SerializeField] private DescriptionUI _descriptionUI = null;
    [SerializeField] private ActionPopupUI _actionPopupUI = null;

    public DescriptionUI DescriptionUI => _descriptionUI;
    public ActionPopupUI ActionPopupUI => _actionPopupUI;

    private CanvasGroup _menuCanvasGroup;

    private void Awake()
    {
        Instance = this;

        _menuCanvasGroup = GetComponent<CanvasGroup>();

        _actionPopupUI.OnItemDescriptionCall += ShowDescription;

        SkillSlotUI.OnSkillDescriptionCall += ShowDescription;
    }

    public void OpenMenu()
    {
        PlayerUI.Instance.TogglePlayerHUD(false);

        ToggleMenuWindow(true);

        GameMaster.Instance.TogglePause(true);
    }

    public void CloseMenu()
    {
        ToggleMenuWindow(false);

        ClearSelection();

        PlayerUI.Instance.TogglePlayerHUD(true);
        GameMaster.Instance.TogglePause(false);
    }

    public void ToggleMenuWindow(bool enable)
    {
        _menuCanvasGroup.alpha = enable ? 1 : 0;
        _menuCanvasGroup.blocksRaycasts = enable;
    }

    public void ClearSelection()
    {
        _descriptionUI.ClearDescriptionText();
        _actionPopupUI.Toggle(false);
    }

    private void ShowDescription(string name, string type, string description)
    {
        _descriptionUI.SetDescriptionText(name, type, description);
    }
}