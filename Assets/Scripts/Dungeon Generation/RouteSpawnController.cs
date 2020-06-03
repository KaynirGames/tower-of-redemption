using System.Collections.Generic;
using UnityEngine;

public class RouteSpawnController : MonoBehaviour
{
    /// <summary>
    /// Список выбранных позиций на сетке координат подземелья.
    /// </summary>
    public List<Vector2Int> SelectedRoute { get; set; } = new List<Vector2Int>();
    /// <summary>
    /// Список возможных направлений для создания коридора: вверх, вправо, вниз и влево.
    /// </summary>
    private readonly List<Vector2Int> routeDirectionMap = new List<Vector2Int>()
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };
    /// <summary>
    /// Начать формирование коридоров для этажа подземелья.
    /// </summary>
    /// <param name="stage">Данные об этаже подземелья.</param>
    public void InitializeRouteSpawn(DungeonStage stage)
    {
        SelectedRoute.Add(Vector2Int.zero); // Добавим позицию для начальной комнаты.

        int routeLength = Random.Range(stage.MinRouteLength, stage.MaxRouteLength + 1);
        CreateRoute(routeLength, stage.RouteSpawnerCount);
    }
    /// <summary>
    /// Сформировать коридоры подземелья.
    /// </summary>
    /// <param name="routeLength">Длина коридоров.</param>
    /// <param name="routeSpawnerCount">Количество создателей коридоров.</param>
    private void CreateRoute(int routeLength, int routeSpawnerCount)
    {
        List<RouteSpawner> routeSpawners = new List<RouteSpawner>();
        // Заполняем список создателей коридоров.
        for (int i = 0; i < routeSpawnerCount; i++)
        {
            routeSpawners.Add(new RouteSpawner(Vector2Int.zero));
        }
        // Формируем коридоры с помощью создателей, попутно заполняя список посещенных точек в сетке подземелья.
        foreach (RouteSpawner routeSpawner in routeSpawners)
        {
            for (int i = 1; i < routeLength; i++)
            {
                Vector2Int newPos = routeSpawner.NextMove(routeDirectionMap);

                if (!DoesPositionTaken(newPos))
                {
                    SelectedRoute.Add(newPos);
                }
            }
        }
    }
    /// <summary>
    /// Занята ли позиция в сетке подземелья?
    /// </summary>
    private bool DoesPositionTaken(Vector2Int gridPosition)
    {
        return SelectedRoute.Exists(pos => pos == gridPosition);
    }
}
