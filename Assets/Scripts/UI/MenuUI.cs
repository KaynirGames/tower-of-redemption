using TMPro;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _menuTabsParent = null;
    [SerializeField] private GameObject _selectionCursor = null;
    [SerializeField] private DescriptionUI _descriptionUI = null;
    [SerializeField] private TextMeshProUGUI _tabHeaderText = null;

    private Canvas _menuCanvas;
    private MenuTabUI[] _menuTabs;
    private int _currentTabIndex = 0;

    private void Awake()
    {
        SelectionHandlerUI.OnCursorCall += ShowSelectionCursor;
        SelectionHandlerUI.OnSelectionCancel += CancelSelection;
        BookSlotUI.OnSkillDescriptionCall += ShowDescription;

        _menuCanvas = GetComponent<Canvas>();
        _menuTabs = _menuTabsParent.GetComponentsInChildren<MenuTabUI>();
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
        _menuCanvas.enabled = true;
        _menuTabs[_currentTabIndex].Toggle(true);
        _tabHeaderText.SetText(_menuTabs[_currentTabIndex].TabName);
        GameMaster.Instance.TogglePause(true);
    }

    public void CloseMenu()
    {
        _menuCanvas.enabled = false;
        _menuTabs[_currentTabIndex].Toggle(false);
        CancelSelection();
        GameMaster.Instance.TogglePause(false);
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
        _descriptionUI.FillDescriptionPanel(name, type, description);
    }

    private void CancelSelection()
    {
        _selectionCursor.SetActive(false);
        _descriptionUI.ClearDescriptionPanel();
    }
}