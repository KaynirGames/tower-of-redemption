using System.Collections.Generic;
using UnityEngine;

public class RouteSpawnController : MonoBehaviour
{
    [SerializeField] private List<Vector2Int> selectedRoute = new List<Vector2Int>(); // Выбранные позиции (для отображения в инспекторе).
    [SerializeField] private List<Vector2Int> potentialBossLocation = new List<Vector2Int>(); // Позиции комнаты с боссом (для отображения в инспекторе).
    /// <summary>
    /// Список выбранных позиций на сетке координат подземелья.
    /// </summary>
    public List<Vector2Int> SelectedRoute => selectedRoute;
    /// <summary>
    /// Список потенциальных позиций для комнаты с боссом.
    /// </summary>
    public List<Vector2Int> PotentialBossLocation => potentialBossLocation;
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
        selectedRoute.Add(Vector2Int.zero); // Добавим позицию для начальной комнаты.

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
                Vector2Int newPos = routeSpawner.NextMove(routeDirectionMap);

                // Запоминаем позицию, если она не занята.
                if (!DoesPositionTaken(newPos))
                {
                    selectedRoute.Add(newPos);
                }
            }

            // Запоминаем последнюю позицию текущего создателя как потенциальное место для комнаты с боссом.
            Vector2Int lastPos = selectedRoute[selectedRoute.Count - 1];
            potentialBossLocation.Add(lastPos);
        }
    }
    /// <summary>
    /// Занята ли позиция в сетке подземелья?
    /// </summary>
    private bool DoesPositionTaken(Vector2Int gridPosition)
    {
        return selectedRoute.Exists(pos => pos == gridPosition);
    }
}
