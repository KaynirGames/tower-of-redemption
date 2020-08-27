using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(RouteSpawnController))]
public class DungeonStageManager : MonoBehaviour
{
    /// <summary>
    /// Событие на загрузку этажа подземелья.
    /// </summary>
    public static event Action OnStageLoadComplete = delegate { };
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
    /// Точка на сетке координат подземелья, куда загружается комната в настоящий момент.
    /// </summary>
    private GridPoint _currentGridPoint;
    /// <summary>
    /// Выбранные позиции на сетке подземелья.
    /// </summary>
    private List<Vector2Int> _selectedRoute;

    private void Awake()
    {
        _routeController = GetComponent<RouteSpawnController>();
        _gridPointQueue = new Queue<GridPoint>();
    }

    private void Start()
    {
        Room.OnRoomLoadComplete += RegisterRoom;
        CreateDungeonStage(currentStage);
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
            RoomType randomType = (RoomType)dungeonStage.OptionalRooms.ChooseRandomObject();
            CreateGridPoint(randomType, _selectedRoute[i]);
        }

        TryLoadNext();
    }
    /// <summary>
    /// Создать новую точку на сетке подземелья.
    /// </summary>
    /// <param name="roomType">Данные о типе комнаты.</param>
    /// <param name="gridPosition">Позиция на сетке координат подземелья.</param>
    private void CreateGridPoint(RoomType roomType, Vector2Int gridPosition)
    {
        string roomSceneName = string.Concat(currentStage.StageSceneName, "_", roomType.RoomSceneName);
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
        if (_gridPointQueue.Count > 0)
        {
            _currentGridPoint = _gridPointQueue.Dequeue();
            StartCoroutine(LoadRoomScene(_currentGridPoint.RoomScene));
        }
        else
        {
            OnStageLoadComplete?.Invoke();
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

        TryLoadNext();
    }
}
