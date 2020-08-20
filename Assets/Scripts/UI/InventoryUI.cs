using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("Основные компоненты инвентаря:")]
    [SerializeField] private GameObject _inventoryWindow = null; // Окно инвентаря.
    [SerializeField] private GameObject _nextTabButton = null; // Кнопка перехода к следующей вкладке.
    [SerializeField] private GameObject _previousTabButton = null; // Кнопка перехода к предыдущей вкладке.
    [SerializeField] private InventoryTabUI[] _inventoryTabs = null; // Вкладки инвентаря.
    [Header("Компоненты для отображения спека игрока:")]
    [SerializeField] private SkillDisplayUI _playerSkillDisplay = null;
    [SerializeField] private Image _playerSpecIcon = null;

    private TextMeshProUGUI _nextTabNameField = null; // Поле с названием следующей вкладки.
    private TextMeshProUGUI _previousTabNameField = null; // Поле с названием предыдущей вкладки.

    private int _currentTabIndex = 0; // Индекс текущей вкладки инвентаря.

    private void Awake()
    {
        _nextTabNameField = _nextTabButton.GetComponentInChildren<TextMeshProUGUI>();
        _previousTabNameField = _previousTabButton.GetComponentInChildren<TextMeshProUGUI>();

        Player.OnPlayerActive += Init;
    }
    /// <summary>
    /// Открыть инвентарь.
    /// </summary>
    public void OpenInventory()
    {
        _inventoryWindow.SetActive(true);
        ChangeTabStatus(_currentTabIndex, true);
        GameMaster.Instance.TogglePause(true);
    }
    /// <summary>
    /// Закрыть инвентарь.
    /// </summary>
    public void CloseInventory()
    {
        _inventoryWindow.SetActive(false);
        GameMaster.Instance.TogglePause(false);
    }
    /// <summary>
    /// Перейти к следующей вкладке инвентаря.
    /// </summary>
    public void MoveNextTab()
    {
        ChangeTabStatus(_currentTabIndex, false);
        _currentTabIndex++;
        SetTab(_currentTabIndex);
    }
    /// <summary>
    /// Перейти к предыдущей вкладке инвентаря.
    /// </summary>
    public void MovePreviousTab()
    {
        ChangeTabStatus(_currentTabIndex, false);
        _currentTabIndex--;
        SetTab(_currentTabIndex);
    }
    /// <summary>
    /// Инициализировать UI инвентаря.
    /// </summary>
    private void Init(Player player)
    {
        _playerSkillDisplay.Init(player.SkillBook);
        _playerSpecIcon.sprite = player.PlayerSpec.Icon;
        _playerSpecIcon.enabled = true;

        SetTab(_currentTabIndex);
    }
    /// <summary>
    /// Активирует вкладку инвентаря с выбранным индексом.
    /// </summary>
    private void SetTab(int tabIndex)
    {
        ChangeTabStatus(tabIndex, true);

        if (tabIndex - 1 >= 0)
        {
            _previousTabButton.SetActive(true);
            _previousTabNameField.SetText(_inventoryTabs[tabIndex - 1].Title);
        }
        else { _previousTabButton.SetActive(false); }

        if (tabIndex + 1 < _inventoryTabs.Length)
        {
            _nextTabButton.SetActive(true);
            _nextTabNameField.SetText(_inventoryTabs[tabIndex + 1].Title);
        }
        else { _nextTabButton.SetActive(false); }
    }
    /// <summary>
    /// Включить/выключить вкладку инвентаря.
    /// </summary>
    private void ChangeTabStatus(int tabIndex, bool isActive)
    {
        if (isActive)
        {
            _inventoryTabs[tabIndex].Activate();
        }
        else
        {
            _inventoryTabs[tabIndex].Deactivate();
        }
    }
}