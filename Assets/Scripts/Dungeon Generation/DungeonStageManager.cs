using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonStageManager : MonoBehaviour
{
    [SerializeField] private DungeonStage currentStage = null; // Информация о текущем этаже подземелья.
    [SerializeField] private RoomRuntimeSet loadedStageRooms = null; // Набор комнат, загруженных на этаже подземелья.
    [SerializeField] private RouteSpawnController routeController = null; // Контроллер, управляющий созданием коридоров на этаже.

    /// <summary>
    /// Очередь загрузки комнат на сетке координат подземелья.
    /// </summary>
    private readonly Queue<GridRoom> gridRoomQueue = new Queue<GridRoom>();
    /// <summary>
    /// Комната на сетке координат подземелья, которая загружается в настоящий момент.
    /// </summary>
    private GridRoom currentGridRoom;
    /// <summary>
    /// Происходит ли загрузка комнаты в настоящий момент?
    /// </summary>
    private bool isLoadingRoom;
    /// <summary>
    /// Текущий список выбранных позиций на сетке координат подземелья.
    /// </summary>
    private List<Vector2Int> currentSelectedRoute;

    private void Start()
    {
        CreateDungeonStage(currentStage);
    }

    private void Update()
    {
        UpdateRoomQueue();
    }
    /// <summary>
    /// Сформировать весь этаж подземелья.
    /// </summary>
    /// <param name="dungeonStage">Информация об этаже подземелья.</param>
    public void CreateDungeonStage(DungeonStage dungeonStage)
    {
        routeController.InitializeRouteSpawn(dungeonStage);
        currentSelectedRoute = routeController.SelectedRoute;

        SpawnStartRoom();

        SpawnBossRoom(routeController.PotentialBossLocation);

        for (int i = 0; i < currentSelectedRoute.Count; i++)
        {
            RoomTypeData randomType = (RoomTypeData)dungeonStage.OptionalRoomTypes.ChooseRandom();

            if (randomType == null) return;

            CreateGridRoom(randomType, currentSelectedRoute[i]);
        }
    }
    /// <summary>
    /// Загрузить обязательную стартовую комнату.
    /// </summary>
    private void SpawnStartRoom()
    {
        Vector2Int position = currentSelectedRoute[0];
        CreateGridRoom(currentStage.StartRoomType, position);
        currentSelectedRoute.Remove(position);
    }
    /// <summary>
    /// Загрузить обязательную комнату с боссом.
    /// </summary>
    private void SpawnBossRoom(List<Vector2Int> potentialBossLocation)
    {
        Vector2Int position = potentialBossLocation[Random.Range(0, potentialBossLocation.Count)];
        CreateGridRoom(currentStage.BossRoomType, position);
        currentSelectedRoute.Remove(position);
    }
    /// <summary>
    /// Загрузить комнаты в порядке очереди.
    /// </summary>
    private void UpdateRoomQueue()
    {
        if (isLoadingRoom || gridRoomQueue.Count == 0)
            return;

        currentGridRoom = gridRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoom(currentGridRoom));
    }
    /// <summary>
    /// Создать новую комнату на сетке координат подземелья.
    /// </summary>
    /// <param name="roomTypeData">Данные о типе комнаты.</param>
    /// <param name="gridPosition">Позиция на сетке координат подземелья.</param>
    private void CreateGridRoom(RoomTypeData roomTypeData, Vector2Int gridPosition)
    {
        GridRoom newGridRoom = new GridRoom
        {
            RoomSceneName = $"{currentStage.Name}_{roomTypeData.Name}",
            GridPosition = gridPosition
        };

        gridRoomQueue.Enqueue(newGridRoom);
    }
    /// <summary>
    /// Присвоить комнате место на этаже подземелья (отклик на событие по окончанию загрузки комнаты).
    /// </summary>
    public void InitializeRoom()
    {
        if (loadedStageRooms.GetAmount() == 0)
        {
            Debug.Log("Набор загруженных комнат пуст!");
            return;
        }

        Room currentRoom = loadedStageRooms.Find(room => !room.IsInitialized);

        if (currentRoom == null)
            return;

        currentRoom.DungeonGridPosition = currentGridRoom.GridPosition;
        currentRoom.SetWorldPosition();
        currentRoom.IsInitialized = true;

        isLoadingRoom = false;
    }
    /// <summary>
    /// Асинхронная загрузка сцены с комнатой.
    /// </summary>
    private IEnumerator LoadRoom(GridRoom gridRoom)
    {
        AsyncOperation roomLoadAsync = SceneManager.LoadSceneAsync(gridRoom.RoomSceneName, LoadSceneMode.Additive);

        while (!roomLoadAsync.isDone)
        {
            yield return null;
        }
    }
}
