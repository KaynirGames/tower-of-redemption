using UnityEngine;

/// <summary>
/// Класс для отображения ресурсов на UI.
/// </summary>
public class ResourceDisplayUI : MonoBehaviour
{
    [SerializeField] private ResourceBarUI _healthBar = null;
    [SerializeField] private ResourceBarUI _spiritBar = null;

    private CharacterResource _health;
    private CharacterResource _spirit;

    public void RegisterResources(CharacterStats stats)
    {
        _health = stats.Health;
        _spirit = stats.Spirit;

        _healthBar.RegisterResource(_health);
        _spiritBar.RegisterResource(_spirit);

        _health.OnChange += _healthBar.UpdateBar;
        _spirit.OnChange += _spiritBar.UpdateBar;
    }

    public void ClearResourceUI()
    {
        _health.OnChange -= _healthBar.UpdateBar;
        _spirit.OnChange -= _spiritBar.UpdateBar;

        _healthBar.Clear();
        _spiritBar.Clear();

        _health = null;
        _spirit = null;
    }
}