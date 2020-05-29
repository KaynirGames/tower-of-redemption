using UnityEngine;

/// <summary>
/// Промежуточный класс, олицетворяющий комнату на сетке координат подземелья.
/// </summary>
public class GridRoom
{
    /// <summary>
    /// Название сцены с комнатой, складывается из названий этажа и типа комнаты.
    /// </summary>
    public string RoomSceneName { get; set; }
    /// <summary>
    /// Позиция комнаты на сетке координат подземелья.
    /// </summary>
    public Vector2Int GridPosition { get; set; }
}
