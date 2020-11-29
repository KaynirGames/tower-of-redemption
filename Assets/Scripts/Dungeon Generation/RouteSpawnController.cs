using System.Collections.Generic;
using UnityEngine;

public class RouteSpawnController : MonoBehaviour
{
    public Vector2Int RandomBossPosition => _bossPositions[Random.Range(0, _bossPositions.Count)];

    private List<Vector2Int> _selectedRoute;
    private List<Vector2Int> _bossPositions = new List<Vector2Int>();

    private List<Vector2Int> _routeDirectionMap = new List<Vector2Int>()
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    public List<Vector2Int> RequestRoute(DungeonStage stage)
    {
        _selectedRoute = new List<Vector2Int>
        {
            Vector2Int.zero
        };

        CreateRoute(stage.RandomRouteLength, stage.RouteSpawnerCount);

        return _selectedRoute;
    }

    private void CreateRoute(int routeLength, int routeSpawnerCount)
    {
        List<RouteSpawner> routeSpawners = new List<RouteSpawner>();

        for (int i = 0; i < routeSpawnerCount; i++)
        {
            routeSpawners.Add(new RouteSpawner());
        }

        foreach (RouteSpawner routeSpawner in routeSpawners)
        {
            for (int i = 0; i < routeLength; i++)
            {
                Vector2Int newPos = routeSpawner.MoveNext(_routeDirectionMap);

                if (!IsOccupied(newPos))
                {
                    _selectedRoute.Add(newPos);
                }
            }

            Vector2Int bossPos = _selectedRoute[_selectedRoute.Count - 1];
            _bossPositions.Add(bossPos);
        }
    }

    private bool IsOccupied(Vector2Int gridPosition)
    {
        return _selectedRoute.Exists(pos => pos == gridPosition);
    }
}
