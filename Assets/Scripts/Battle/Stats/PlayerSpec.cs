using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Специализация персонажа.
/// </summary>
[CreateAssetMenu(fileName = "NewPlayerSpec", menuName = "Scriptable Objects/Battle/Specs/Player Spec")]
public class PlayerSpec : BaseStats
{
    [Header("Информация о спеке игрока:")]
    [TextArea(10, 10)]
    [SerializeField] private string _description = "New Spec Description";
    /// <summary>
    /// Краткое описание специализации игрока.
    /// </summary>
    public string Description => _description;

    // Базовый набор абилок
}
