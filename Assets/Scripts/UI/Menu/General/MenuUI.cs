using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public static MenuUI Instance { get; private set; }

    [SerializeField] private DescriptionUI _descriptionUI = null;
    [SerializeField] private ActionPopupUI _actionPopupUI = null;
    [SerializeField] private SkillReplaceUI _skillReplaceUI = null;

    public DescriptionUI DescriptionUI => _descriptionUI;
    public ActionPopupUI ActionPopupUI => _actionPopupUI;

    public bool InMenu { get; private set; }

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
        GameMaster.Instance.TogglePause(true);

        ToggleMenuWindow(true);
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
        InMenu = enable;

        _menuCanvasGroup.alpha = enable ? 1 : 0;
        _menuCanvasGroup.blocksRaycasts = enable;
    }

    public void ClearSelection()
    {
        _descriptionUI.ClearDescriptionText();
        _actionPopupUI.Toggle(false);
    }

    public void ShowSkillReplaceWindow(SkillSO skillSO, System.Action<int> onConfirm)
    {
        _skillReplaceUI.ShowSkillReplaceWindow(skillSO, onConfirm);
    }

    private void ShowDescription(string name, string type, string description)
    {
        _descriptionUI.SetDescriptionText(name, type, description);
    }
}