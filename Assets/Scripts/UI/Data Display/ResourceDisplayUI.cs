using UnityEngine;

/// <summary>
/// Класс для отображения ресурсов на UI.
/// </summary>
public class ResourceDisplayUI : MonoBehaviour
{
    [SerializeField] private ResourceBarUI _healthBar = null; // Полоса здоровья персонажа.
    [SerializeField] private ResourceBarUI _energyBar = null; // Полоса энергии персонажа.

    private CharacterResource _health;
    private CharacterResource _energy;

    public void RegisterResources(CharacterStats stats)
    {
        _health = stats.Health;
        _energy = stats.Energy;

        _healthBar.RegisterResource(_health);
        _energyBar.RegisterResource(_energy);

        _health.OnChange += _healthBar.UpdateBar;
        _energy.OnChange += _energyBar.UpdateBar;
    }

    private void OnDestroy()
    {
        _health.OnChange -= _healthBar.UpdateBar;
        _energy.OnChange -= _energyBar.UpdateBar;
    }
}