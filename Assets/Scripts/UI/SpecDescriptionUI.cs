using TMPro;
using UnityEngine;

/// <summary>
/// Описание спека персонажа на UI.
/// </summary>
public class SpecDescriptionUI : MonoBehaviour
{
    [SerializeField] private GameObject _specDescriptionParent = null;
    [Header("Отображение данных о спеке:")]
    [SerializeField] private TextMeshProUGUI _nameField = null;
    [SerializeField] private TextMeshProUGUI _descriptionField = null;
    [SerializeField] private ResourceDisplayUI _resourceDisplay = null;
    [SerializeField] private StatDisplayUI _statDisplay = null;
    [SerializeField] private EfficacyDisplayUI _efficacyDisplay = null;
    /// <summary>
    /// Показать описание спека персонажа.
    /// </summary>
    public void ShowDescription()
    {
        _specDescriptionParent.SetActive(true);
    }
    /// <summary>
    /// Скрыть описание спека персонажа.
    /// </summary>
    public void HideDescription()
    {
        _specDescriptionParent.SetActive(false);
    }
    /// <summary>
    /// Инициализировать описание спека игрока.
    /// </summary>
    public void Init(CharacterStats characterStats, PlayerSpec playerSpec)
    {
        InitStats(characterStats);

        _nameField.SetText(playerSpec.SpecName);
        _descriptionField.SetText(playerSpec.Description);
    }
    /// <summary>
    /// Инициализировать описание спека противника.
    /// </summary>
    public void Init(CharacterStats characterStats, EnemySpec enemySpec)
    {
        InitStats(characterStats);

        _nameField.SetText(enemySpec.Name);
    }
    /// <summary>
    /// Инициализировать описание статов персонажа.
    /// </summary>
    private void InitStats(CharacterStats characterStats)
    {
        _resourceDisplay.Init(characterStats);
        _statDisplay.Init(characterStats);
        _efficacyDisplay.Init(characterStats);
    }
}
