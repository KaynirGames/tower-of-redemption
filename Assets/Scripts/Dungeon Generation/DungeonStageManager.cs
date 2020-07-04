using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(RouteSpawnController))]
public class DungeonStageManager : MonoBehaviour
{
    /// <summary>
    /// Событие на загрузку этажа подземелья.
    /// </summary>
    public static event Action OnStageLoaded = delegate { };
    /// <summary>
    /// Точка на сетке подземелья для будущей комнаты.
    /// </summary>
    private struct GridPoint
    {
        /// <summary>
        /// Название сцены с комнатой.
        /// </summary>
        public string RoomScene { get; private set; }
        /// <summary>
        /// Позиция на сетке подземелья.
        /// </summary>
        public Vector2Int GridPosition { get; private set; }

        public GridPoint(string roomScene, Vector2Int gridPos)
        {
            RoomScene = roomScene;
            GridPosition = gridPos;
        }
    }

    [SerializeField] private DungeonStage currentStage = null; // Информация о текущем этаже подземелья.
    /// <summary>
    /// Контроллер, управляющий созданием коридоров на этаже.
    /// </summary>
    private RouteSpawnController _routeController;
    /// <summary>
    /// Очередь точек на сетке координат подземелья для загрузки комнат.
    /// </summary>
    private Queue<GridPoint> _gridPointQueue;
    /// <summary>
    /// Точка на сетке координат подземелья, куда в настоящий момент загружается комната.
    /// </summary>
    private GridPoint _currentGridPoint;
    /// <summary>
    /// Происходит ли загрузка комнаты в настоящий момент?
    /// </summary>
    private bool _isLoadingRoom;
    /// <summary>
    /// Выбранные позиции на сетке подземелья.
    /// </summary>
    private List<Vector2Int> _selectedRoute;
    /// <summary>
    /// Загрузились ли все комнаты на этаже?
    /// </summary>
    private bool _stageLoadComplete = false;

    private void Awake()
    {
        _routeController = GetComponent<RouteSpawnController>();
        _gridPointQueue = new Queue<GridPoint>();
    }

    private void Start()
    {
        Room.OnRoomLoaded += RegisterRoom;
        CreateDungeonStage(currentStage);
    }

    private void Update()
    {
        TryLoadNext();
    }
    /// <summary>
    /// Сформировать все комнаты на этаже подземелья.
    /// </summary>
    /// <param name="dungeonStage">Информация об этаже подземелья.</param>
    public void CreateDungeonStage(DungeonStage dungeonStage)
    {      
        _selectedRoute = _routeController.RequestRoute(dungeonStage);

        SpawnSpecificRoom(currentStage.StartRoom, _selectedRoute[0]);
        SpawnSpecificRoom(currentStage.BossRoom, _routeController.RandomBossLocation);

        for (int i = 0; i < _selectedRoute.Count; i++)
        {
            RoomType randomType = (RoomType)dungeonStage.OptionalRooms.ChooseRandom();
            CreateGridPoint(randomType, _selectedRoute[i]);
        }
    }
    /// <summary>
    /// Создать новую точку на сетке подземелья.
    /// </summary>
    /// <param name="roomType">Данные о типе комнаты.</param>
    /// <param name="gridPosition">Позиция на сетке координат подземелья.</param>
    private void CreateGridPoint(RoomType roomType, Vector2Int gridPosition)
    {
        string roomSceneName = string.Concat(currentStage.Name, "_", roomType.Name);
        GridPoint newGridPoint = new GridPoint(roomSceneName, gridPosition);
        _gridPointQueue.Enqueue(newGridPoint);
    }
    /// <summary>
    /// Создать комнату конкретного типа.
    /// </summary>
    private void SpawnSpecificRoom(RoomType roomType, Vector2Int gridPosition)
    {
        CreateGridPoint(roomType, gridPosition);
        _selectedRoute.Remove(gridPosition);
    }
    /// <summary>
    /// Начать загрузку следующей комнаты.
    /// </summary>
    private void TryLoadNext()
    {
        if (_stageLoadComplete)
            return;

        if (!_isLoadingRoom && _gridPointQueue.Count != 0)
        {
            _currentGridPoint = _gridPointQueue.Dequeue();
            _isLoadingRoom = true;
            StartCoroutine(LoadRoomScene(_currentGridPoint.RoomScene));
        }
        else if (!_isLoadingRoom && _gridPointQueue.Count == 0)
        {
            _stageLoadComplete = true;
            OnStageLoaded?.Invoke();
        }
    }
    /// <summary>
    /// Асинхронная загрузка сцены с комнатой.
    /// </summary>
    private IEnumerator LoadRoomScene(string sceneName)
    {
        AsyncOperation roomLoadAsync = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!roomLoadAsync.isDone)
        {
            yield return null;
        }
    }
    /// <summary>
    /// Присвоить комнате место на этаже подземелья.
    /// </summary>
    private void RegisterRoom(Room room)
    {
        room.DungeonGridPosition = _currentGridPoint.GridPosition;
        room.SetWorldPosition();
        _isLoadingRoom = false;
    }
}
