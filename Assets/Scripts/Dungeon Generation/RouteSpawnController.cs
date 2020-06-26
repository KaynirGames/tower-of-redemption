using System.Collections.Generic;
using UnityEngine;

public class RouteSpawnController : MonoBehaviour
{
    /// <summary>
    /// Случайная позиция для босс-комнаты (среди потенциальных).
    /// </summary>
    public Vector2Int RandomBossLocation => _potentialBossLocations[Random.Range(0, _potentialBossLocations.Count)];
    /// <summary>
    /// Выбранные позиции для будущих комнат.
    /// </summary>
    private List<Vector2Int> _selectedRoute;
    /// <summary>
    /// Потенциальные позиции для босс-комнат.
    /// </summary>
    private List<Vector2Int> _potentialBossLocations;
    /// <summary>
    /// Возможные направления для создания коридора: вверх, вправо, вниз и влево.
    /// </summary>
    private List<Vector2Int> _routeDirectionMap = new List<Vector2Int>()
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    private void Awake()
    {
        _selectedRoute = new List<Vector2Int>();
        _potentialBossLocations = new List<Vector2Int>();
    }
    /// <summary>
    /// Запросить коридоры для этажа подземелья.
    /// </summary>
    /// <param name="stage">Данные об этаже подземелья.</param>
    public List<Vector2Int> RequestRoute(DungeonStage stage)
    {
        _selectedRoute.Add(Vector2Int.zero); // Добавим позицию для начальной комнаты.

        int routeLength = Random.Range(stage.MinRouteLength, stage.MaxRouteLength + 1);
        CreateRoute(routeLength, stage.RouteSpawnerCount);

        return _selectedRoute;
    }
    /// <summary>
    /// Сформировать коридоры подземелья.
    /// </summary>
    /// <param name="routeLength">Длина коридоров.</param>
    /// <param name="routeSpawnerCount">Количество создателей коридоров.</param>
    private void CreateRoute(int routeLength, int routeSpawnerCount)
    {
        List<RouteSpawner> routeSpawners = new List<RouteSpawner>();
        // Добавляем создателей коридоров.
        for (int i = 0; i < routeSpawnerCount; i++)
        {
            routeSpawners.Add(new RouteSpawner(Vector2Int.zero));
        }
        // Формируем коридоры с помощью создателей, попутно заполняя список выбранных позиций.
        foreach (RouteSpawner routeSpawner in routeSpawners)
        {
            for (int i = 1; i < routeLength; i++)
            {
                Vector2Int newPos = routeSpawner.MoveNext(_routeDirectionMap);
                // Запоминаем позицию, если она не занята.
                if (!DoesPositionTaken(newPos))
                {
                    _selectedRoute.Add(newPos);
                }
            }
            // Запоминаем конечную позицию текущего создателя (потенциальное место для босс-комнат).
            Vector2Int endPos = _selectedRoute[_selectedRoute.Count - 1];
            _potentialBossLocations.Add(endPos);
        }
    }
    /// <summary>
    /// Занята ли позиция в сетке подземелья?
    /// </summary>
    private bool DoesPositionTaken(Vector2Int gridPosition)
    {
        return _selectedRoute.Exists(pos => pos == gridPosition);
    }
}
