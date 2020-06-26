using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Создатель коридора подземелья.
/// </summary>
public class RouteSpawner
{
    /// <summary>
    /// Позиция на сетке подземелья.
    /// </summary>
    private Vector2Int _gridPosition;
    /// <summary>
    /// Предыдущее выбранное направление.
    /// </summary>
    private Vector2Int _previousDirection;
    /// <summary>
    /// Новый создатель коридора.
    /// </summary>
    /// <param name="initialPos">Начальная позиция.</param>
    public RouteSpawner(Vector2Int initialPos)
    {
        _gridPosition = initialPos;
        _previousDirection = Vector2Int.zero;
    }
    /// <summary>
    /// Перейти на следующую позицию в сетке подземелья.
    /// </summary>
    /// <param name="routeDirectionMap">Возможные направления движения.</param>
    /// <returns></returns>
    public Vector2Int MoveNext(List<Vector2Int> routeDirectionMap)
    {
        // Убираем направление, противоположное предыдущему, чтобы избежать шага назад.
        List<Vector2Int> newRouteMap = new List<Vector2Int>();
        newRouteMap.AddRange(routeDirectionMap);
        newRouteMap.Remove(-_previousDirection);

        // Выбираем новое направление и соответствующую позицию.
        Vector2Int newDirection = newRouteMap[Random.Range(0, newRouteMap.Count)];
        _gridPosition += newDirection;

        // Обновляем предыдущее направление.
        _previousDirection = newDirection;

        return _gridPosition;
    }
}
