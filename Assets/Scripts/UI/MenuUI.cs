using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [Header("Основные объекты меню:")]
    [SerializeField] private GameObject _menuBackground = null;
    [SerializeField] private MenuTabUI[] _menuTabs = null;
    [Header("Отображающие объекты:")]
    [SerializeField] private GameObject _selectionCursor = null;
    [SerializeField] private DescriptionUI _descriptionUI = null;

    private int _currentTabIndex = 0;

    private void Awake()
    {
        SelectionHandlerUI.OnCursorCall += ShowSelectionCursor;
        SelectionHandlerUI.OnSelectionCancel += CancelSelection;
        BookSlotUI.OnSkillDescriptionCall += ShowDescription;
    }

    public void OpenTab(int tabIndex)
    {
        CloseTab(_currentTabIndex);
        _currentTabIndex = tabIndex;

        _menuTabs[tabIndex].Open();
    }

    public void OpenMenu()
    {
        _menuBackground.SetActive(true);
        _menuTabs[_currentTabIndex].Toggle(true);
        GameMaster.Instance.TogglePause(true);
    }

    public void CloseMenu()
    {
        _menuBackground.SetActive(false);
        _menuTabs[_currentTabIndex].Toggle(false);
        GameMaster.Instance.TogglePause(false);
    }

    private void CloseTab(int tabIndex)
    {
        _menuTabs[tabIndex].Close();
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