using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RouteSpawnController))]
public class DungeonStageGenerator : MonoBehaviour
{
    public static event Action<DungeonStage> OnStageLoadComplete = delegate { };

    private RouteSpawnController _routeController;
    private List<Vector2Int> _selectedRoute;

    private void Awake()
    {
        _routeController = GetComponent<RouteSpawnController>();
    }

    public IEnumerator LoadDungeonStage(DungeonStage stage)
    {
        _selectedRoute = _routeController.RequestRoute(stage);

        SpawnRoom(stage.StartRoomPrefab, _selectedRoute[0], true);
        SpawnRoom(stage.BossRoomPrefab, _routeController.RandomBossPosition, true);

        for (int i = 0; i < _selectedRoute.Count; i++)
        {
            SpawnRoom(stage.RandomOptionalRoomPrefab, _selectedRoute[i]);
            yield return null;
        }

        Instantiate(stage.GlobalLightPrefab, Vector3.zero, Quaternion.identity);
        yield return null;

        Room.LoadedRooms.ForEach(room => room.PrepareRoom());
    }

    private void SpawnRoom(GameObject roomPrefab, Vector2Int gridPosition, bool removePositionFromRoute = false)
    {
        GameObject roomObject = Instantiate(roomPrefab);
        roomObject.GetComponent<Room>().SetRoomPosition(gridPosition);

        if (removePositionFromRoute)
        {
            _selectedRoute.Remove(gridPosition);
        }
    }
}