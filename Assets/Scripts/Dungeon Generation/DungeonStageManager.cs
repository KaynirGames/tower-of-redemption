using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(RouteSpawnController))]
public class DungeonStageManager : MonoBehaviour
{
    public static event Action OnStageLoadComplete = delegate { };

    private struct GridPoint
    {
        public string RoomScene { get; private set; }
        public Vector2Int GridPosition { get; private set; }

        public GridPoint(string roomScene, Vector2Int gridPos)
        {
            RoomScene = roomScene;
            GridPosition = gridPos;
        }
    }

    [SerializeField] private DungeonStage _currentStage = null; // Информация о текущем этаже подземелья.
    [SerializeField] private Transform _battlefieldPlacement = null;

    private RouteSpawnController _routeSpawnController;

    private Queue<GridPoint> _gridPointQueue;
    private GridPoint _currentGridPoint;

    private List<Vector2Int> _selectedRoute;

    private void Awake()
    {
        _routeSpawnController = GetComponent<RouteSpawnController>();
        _gridPointQueue = new Queue<GridPoint>();

        Room.OnRoomLoadComplete += RegisterRoom;
    }

    private void Start()
    {
        CreateDungeonStage(_currentStage);

        SpawnBattlefield(_currentStage);
    }

    public void CreateDungeonStage(DungeonStage dungeonStage)
    {
        _selectedRoute = _routeSpawnController.RequestRoute(dungeonStage);

        SpawnSpecificRoom(_currentStage.StartRoom, _selectedRoute[0]);
        SpawnSpecificRoom(_currentStage.BossRoom, _routeSpawnController.RandomBossLocation);

        for (int i = 0; i < _selectedRoute.Count; i++)
        {
            RoomType randomType = (RoomType)dungeonStage.OptionalRooms.ChooseRandomObject();
            CreateGridPoint(randomType, _selectedRoute[i]);
        }

        TryLoadNextRoom();

        SpawnBattlefield(dungeonStage);
    }

    private void CreateGridPoint(RoomType roomType, Vector2Int gridPosition)
    {
        string roomSceneName = string.Concat(_currentStage.SceneNamePrefix, "_", roomType.RoomSceneName);
        GridPoint newGridPoint = new GridPoint(roomSceneName, gridPosition);
        _gridPointQueue.Enqueue(newGridPoint);
    }

    private void SpawnSpecificRoom(RoomType roomType, Vector2Int gridPosition)
    {
        CreateGridPoint(roomType, gridPosition);
        _selectedRoute.Remove(gridPosition);
    }

    private void TryLoadNextRoom()
    {
        if (_gridPointQueue.Count > 0)
        {
            _currentGridPoint = _gridPointQueue.Dequeue();
            StartCoroutine(LoadRoomSceneAsync(_currentGridPoint.RoomScene));
        }
        else
        {
            OnStageLoadComplete?.Invoke();
        }
    }

    private IEnumerator LoadRoomSceneAsync(string sceneName)
    {
        AsyncOperation roomLoadAsync = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!roomLoadAsync.isDone)
        {
            yield return null;
        }
    }

    private void RegisterRoom(Room room)
    {
        room.DungeonGridPosition = _currentGridPoint.GridPosition;
        room.SetWorldPosition();

        TryLoadNextRoom();
    }

    private void SpawnBattlefield(DungeonStage dungeonStage)
    {
        Instantiate(dungeonStage.BattlefieldBackground,
                    _battlefieldPlacement.position,
                    _battlefieldPlacement.rotation,
                    _battlefieldPlacement);
    }
}