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
    [Header("Отображение спека игрока:")]
    [SerializeField] private SpecDescriptionUI _playerSpecDescription = null;
    [SerializeField] private Image _playerSpecIcon = null;
    [Header("Отображение умений игрока:")]
    [SerializeField] private SkillDescriptionUI _playerSkillDescription = null;
    [SerializeField] private SkillBookUI _playerSkillDisplay = null;

    private TextMeshProUGUI _nextTabNameField = null; // Поле с названием следующей вкладки.
    private TextMeshProUGUI _previousTabNameField = null; // Поле с названием предыдущей вкладки.

    private int _currentTabIndex = 0; // Индекс текущей вкладки инвентаря.

    private void Awake()
    {
        _nextTabNameField = _nextTabButton.GetComponentInChildren<TextMeshProUGUI>();
        _previousTabNameField = _previousTabButton.GetComponentInChildren<TextMeshProUGUI>();

        PlayerCharacter.OnPlayerActive += Init;
        BookSlotUI.OnDescriptionPanelRequest += ShowPlayerSkillDescription;
    }
    /// <summary>
    /// Открыть инвентарь.
    /// </summary>
    public void OpenInventory()
    {
        _inventoryWindow.SetActive(true);
        _inventoryTabs[_currentTabIndex].Activate();
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
    /// Сменить вкладку инвентаря.
    /// </summary>
    public void SwitchTab(int indexStep)
    {
        _inventoryTabs[_currentTabIndex].Deactivate();
        _currentTabIndex += indexStep;
        ActivateTab(_currentTabIndex);
    }
    /// <summary>
    /// Показать описание спека игрока.
    /// </summary>
    public void ShowPlayerSpecDescription()
    {
        _playerSkillDescription.HideDescription();
        _playerSpecDescription.ShowDescription();
    }
    /// <summary>
    /// Показать описание умения игрока.
    /// </summary>
    private void ShowPlayerSkillDescription(Skill skill)
    {
        _playerSpecDescription.HideDescription();
        _playerSkillDescription.ShowDescription(skill);
    }
    /// <summary>
    /// Инициализировать UI инвентаря.
    /// </summary>
    private void Init(PlayerCharacter player)
    {
        _playerSpecDescription.InitDescription(player.Stats, player.PlayerSpec);
        _playerSkillDisplay.Init(player.SkillBook);
        _playerSpecIcon.sprite = player.PlayerSpec.PlayerSpecIcon;
        _playerSpecIcon.enabled = true;

        ActivateTab(_currentTabIndex);
    }
    /// <summary>
    /// Активирует вкладку инвентаря с выбранным индексом.
    /// </summary>
    private void ActivateTab(int tabIndex)
    {
        _inventoryTabs[tabIndex].Activate();

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
}