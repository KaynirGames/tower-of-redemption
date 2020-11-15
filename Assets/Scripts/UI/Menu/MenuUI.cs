using TMPro;
using UnityEngine;
using UnityEngine.Localization.Tables;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _menuTabsParent = null;
    [SerializeField] private GameObject _selectionCursor = null;
    [SerializeField] private TextMeshProUGUI _tabHeaderText = null;
    [SerializeField] private PlayerUI _playerUI = null;
    [SerializeField] private DescriptionUI _descriptionUI = null;

    private CanvasGroup _menuCanvasGroup;
    private MenuTabUI[] _menuTabs;
    private int _currentTabIndex = 0;

    private void Awake()
    {
        _menuCanvasGroup = GetComponent<CanvasGroup>();
        _menuTabs = _menuTabsParent.GetComponentsInChildren<MenuTabUI>();

        SelectionHandlerUI.OnCursorCall += ShowSelectionCursor;
        SelectionHandlerUI.OnSelectionCancel += CancelSelection;

        SkillSlotUI.OnSkillDescriptionCall += ShowDescription;
    }

    public void OpenTab(int tabIndex)
    {
        CloseTab(_currentTabIndex);
        _currentTabIndex = tabIndex;

        _tabHeaderText.SetText(_menuTabs[tabIndex].TabName);
        _menuTabs[tabIndex].Open();
    }

    public void OpenMenu()
    {
        _playerUI.TogglePlayerHUD(false);
        ToggleMenuWindow(true);

        _menuTabs[_currentTabIndex].Toggle(true);
        _tabHeaderText.SetText(_menuTabs[_currentTabIndex].TabName);
        GameMaster.Instance.TogglePause(true);
    }

    public void CloseMenu()
    {
        ToggleMenuWindow(false);
        _playerUI.TogglePlayerHUD(true);

        _menuTabs[_currentTabIndex].Toggle(false);
        CancelSelection();
        GameMaster.Instance.TogglePause(false);
    }

    public void ToggleMenuWindow(bool enable)
    {
        _menuCanvasGroup.alpha = enable ? 1 : 0;
        _menuCanvasGroup.blocksRaycasts = enable;
    }

    private void CloseTab(int tabIndex)
    {
        _menuTabs[tabIndex].Close();
        CancelSelection();
    }

    private void ShowSelectionCursor(Vector3 position)
    {
        _selectionCursor.transform.position = position;
        _selectionCursor.SetActive(true);
    }

    private void ShowDescription(string name, string type, string description)
    {
        _descriptionUI.SetDescriptionText(name, type, description);
    }

    private void CancelSelection()
    {
        _selectionCursor.SetActive(false);
        _descriptionUI.ClearDescriptionText();
    }
}