using UnityEngine;

/// <summary>
/// Точка на сетке координат подземелья, куда загружается сцена с комнатой.
/// </summary>
public class DungeonGridPoint
{
    /// <summary>
    /// Название сцены с комнатой (название этажа + тип комнаты).
    /// </summary>
    public string RoomSceneName { get; set; }
    /// <summary>
    /// Позиция на сетке координат подземелья.
    /// </summary>
    public Vector2Int GridPosition { get; set; }
}
