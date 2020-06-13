using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Специализация персонажа.
/// </summary>
[CreateAssetMenu(fileName = "NewPlayerSpec", menuName = "Scriptable Objects/Stats System/Player Spec")]
public class PlayerSpec : BaseStats
{
    /// <summary>
    /// Краткое описание специализации игрока.
    /// </summary>
    [TextArea(10,10)]
    public string Description;

    // Базовый набор абилок
}
