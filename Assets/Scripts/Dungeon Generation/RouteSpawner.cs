using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Создатель коридора подземелья.
/// </summary>
public class RouteSpawner
{
    private Vector2Int _gridPosition;
    private Vector2Int _previousDirection;

    public RouteSpawner()
    {
        _gridPosition = Vector2Int.zero;
        _previousDirection = Vector2Int.zero;
    }

    public Vector2Int MoveNext(List<Vector2Int> routeDirectionMap)
    {
        List<Vector2Int> currentRouteMap = new List<Vector2Int>(routeDirectionMap);
        currentRouteMap.Remove(-_previousDirection);

        Vector2Int newDirection = currentRouteMap[Random.Range(0, currentRouteMap.Count)];
        
        _gridPosition += newDirection;
        _previousDirection = newDirection;

        return _gridPosition;
    }
}
