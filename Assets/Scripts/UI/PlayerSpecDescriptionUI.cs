using TMPro;
using UnityEngine;

public class PlayerSpecDescriptionUI : MonoBehaviour
{
    [Header("Взаимоотключаемые объекты:")]
    [SerializeField] private GameObject _specDescriptionPanel = null;
    [SerializeField] private GameObject _skillDescriptionPanel = null;
    [Header("Отображение данных о спеке:")]
    [SerializeField] private TextMeshProUGUI _nameField = null;
    [SerializeField] private TextMeshProUGUI _descriptionField = null;
    [SerializeField] private ResourceDisplayUI _resourceDisplay = null;
    [SerializeField] private StatDisplayUI _statDisplay = null;
    [SerializeField] private EfficacyDisplayUI _efficacyDisplay = null;

    private void Awake()
    {
        Player.OnPlayerActive += Init;
    }
    /// <summary>
    /// Показать описание спека персонажа.
    /// </summary>
    public void ShowDescription()
    {
        _skillDescriptionPanel.SetActive(false);
        _specDescriptionPanel.SetActive(true);
    }
    /// <summary>
    /// Инициализация описания спека персонажа.
    /// </summary>
    private void Init(Player player)
    {
        _resourceDisplay.Init(player.PlayerStats);
        _statDisplay.Init(player.PlayerStats);
        _efficacyDisplay.Init(player.PlayerStats);

        _nameField.SetText(player.PlayerSpec.SpecName);
        _descriptionField.SetText(player.PlayerSpec.Description);
    }
}
