using TMPro;
using UnityEngine;

public class TabControllerUI : MonoBehaviour
{
    [SerializeField] private MenuTabUI[] _menuTabs = null;
    [SerializeField] private TextMeshProUGUI _tabHeaderField = null;

    private int _currentTabIndex = 0;

    private void Awake()
    {
        PlayerCharacter.OnPlayerActive += RegisterPlayer;
    }

    public void SetTab(int tabIndex)
    {
        if (tabIndex != _currentTabIndex)
        {
            CloseTab(_currentTabIndex);
            _currentTabIndex = tabIndex;
        }

        if (_tabHeaderField != null)
        {
            _tabHeaderField.SetText(_menuTabs[tabIndex].TabName);
        }

        _menuTabs[tabIndex].Open();
    }

    private void CloseTab(int tabIndex)
    {
        _menuTabs[tabIndex].Close();
    }

    private void RegisterPlayer(PlayerCharacter player)
    {
        foreach (MenuTabUI tab in _menuTabs)
        {
            tab.RegisterPlayer(player);
        }

        SetTab(_currentTabIndex);
    }

    private void OnDestroy()
    {
        PlayerCharacter.OnPlayerActive -= RegisterPlayer;
    }
}
