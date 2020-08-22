using UnityEngine;

/// <summary>
/// Класс для отображения ресурсов на UI.
/// </summary>
public class ResourceDisplayUI : MonoBehaviour
{
    [SerializeField] private ResourceBarUI _healthBar = null; // Полоса здоровья персонажа.
    [SerializeField] private ResourceBarUI _energyBar = null; // Полоса энергии персонажа.

    private CharacterStats _characterStats; // Статы персонажа.
    /// <summary>
    /// Инициализировать отображение ресурсов персонажа.
    /// </summary>
    public void Init(CharacterStats characterStats)
    {
        _characterStats = characterStats;

        _healthBar.Init(characterStats.MaxHealth, characterStats.CurrentHealth);
        _energyBar.Init(characterStats.MaxEnergy, characterStats.CurrentEnergy);

        characterStats.OnHealthChange += _healthBar.UpdateBar;
        characterStats.OnEnergyChange += _energyBar.UpdateBar;
    }

    private void OnDestroy()
    {
        _characterStats.OnHealthChange -= _healthBar.UpdateBar;
        _characterStats.OnEnergyChange -= _energyBar.UpdateBar;
    }
}