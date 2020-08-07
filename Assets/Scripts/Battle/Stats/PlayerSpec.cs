using UnityEngine;

/// <summary>
/// Специализация персонажа.
/// </summary>
[CreateAssetMenu(fileName = "NewPlayerSpec", menuName = "Scriptable Objects/Battle/Specs/Player Spec")]
public class PlayerSpec : BaseStats
{
    [Header("Информация о спеке игрока:")]
    [TextArea(5, 5)]
    [SerializeField] private string _description = string.Empty;
    /// <summary>
    /// Краткое описание специализации игрока.
    /// </summary>
    public string Description => _description;
}
