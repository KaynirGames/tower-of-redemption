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

    public void ShowDescription()
    {
        _specDescriptionParent.SetActive(true);
    }

    public void HideDescription()
    {
        _specDescriptionParent.SetActive(false);
    }

    public void InitDescription(CharacterStats characterStats, PlayerSpec playerSpec)
    {
        InitStatsDisplay(characterStats);

        _nameField.SetText(playerSpec.SpecName);
        _descriptionField.SetText(playerSpec.SpecDescription);
    }

    public void InitDescription(CharacterStats characterStats, EnemySpec enemySpec)
    {
        InitStatsDisplay(characterStats);

        _nameField.SetText(enemySpec.SpecName);
    }

    private void InitStatsDisplay(CharacterStats characterStats)
    {
        _resourceDisplay.RegisterResources(characterStats);
        _statDisplay.RegisterStats(characterStats);
        _efficacyDisplay.RegisterElementEfficacies(characterStats);
    }
}
