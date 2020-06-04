using System.Collections.Generic;
using UnityEngine;

public class RouteSpawner
{
    /// <summary>
    /// Позиция на сетке подземелья.
    /// </summary>
    private Vector2Int gridPosition;
    /// <summary>
    /// Предыдущее выбранное направление.
    /// </summary>
    private Vector2Int previousDirection;
    /// <summary>
    /// Новый создатель коридора.
    /// </summary>
    /// <param name="initialPos">Начальная позиция.</param>
    public RouteSpawner(Vector2Int initialPos)
    {
        gridPosition = initialPos;
        previousDirection = Vector2Int.zero;
    }
    /// <summary>
    /// Перемещение на следующую позицию в сетке подземелья.
    /// </summary>
    /// <param name="routeDirectionMap">Возможные направления движения.</param>
    /// <returns></returns>
    public Vector2Int NextMove(List<Vector2Int> routeDirectionMap)
    {
        // Убираем противоположное предыдущему направление, чтобы избежать шага назад.
        routeDirectionMap.Remove(-previousDirection);

        // Выбираем новое направление и соответствующую позицию.
        Vector2Int newDirection = routeDirectionMap[Random.Range(0, routeDirectionMap.Count)];
        gridPosition += newDirection;

        // Обновляем предыдущее направление.
        previousDirection = newDirection;

        return gridPosition;
    }
}
