using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _menuTabsParent = null;
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

        PlayerCharacter.OnPlayerActive += SetupMenu;
        SkillSlotUI.OnSkillDescriptionCall += ShowDescription;
        SelectionHandlerUI.OnSelectionCancel += CancelSelection;
    }

    public void SetTab(int tabIndex)
    {
        if (tabIndex != _currentTabIndex)
        {
            CloseTab(_currentTabIndex);
            _currentTabIndex = tabIndex;
        }

        _tabHeaderText.SetText(_menuTabs[tabIndex].TabName);
        _menuTabs[tabIndex].Toggle(true);
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

    private void SetupMenu(PlayerCharacter player)
    {
        SetTab(_currentTabIndex);
    }

    private void CloseTab(int tabIndex)
    {
        _menuTabs[tabIndex].Toggle(false);
        CancelSelection();
    }

    private void ShowDescription(string name, string type, string description)
    {
        _descriptionUI.SetDescriptionText(name, type, description);
    }

    private void CancelSelection()
    {
        _descriptionUI.ClearDescriptionText();
    }
}