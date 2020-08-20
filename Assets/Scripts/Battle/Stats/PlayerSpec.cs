using UnityEngine;

/// <summary>
/// Специализация персонажа.
/// </summary>
[CreateAssetMenu(fileName = "NewPlayerSpec", menuName = "Scriptable Objects/Battle/Specs/Player Spec")]
public class PlayerSpec : BaseStats
{
    [Header("Информация о спеке игрока:")]
    [SerializeField] private Sprite _icon = null;
    [SerializeField] private TranslatedText _specName = null;
    [SerializeField] private TranslatedText _descriptionText = null;
    /// <summary>
    /// Название спека игрока.
    /// </summary>
    public string SpecName => _specName.Value;
    /// <summary>
    /// Описание специализации игрока.
    /// </summary>
    public string Description => _descriptionText.Value;
    /// <summary>
    /// Иконка специализации игрока.
    /// </summary>
    public Sprite Icon => _icon;
}
