using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonStageManager : MonoBehaviour
{
    [SerializeField] private DungeonStage currentStage = null; // Информация о текущем этаже подземелья.
    [SerializeField] private RoomRuntimeSet loadedStageRooms = null; // Набор комнат, загруженных на этаже подземелья.
    [SerializeField] private DungeonRouteGenerator routeController = null;

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
    /// Список занятых позиций на сетке координат подземелья.
    /// </summary>
    private List<Vector2Int> takenGridPositions = new List<Vector2Int>();

    private void Start()
    {
        //CreateGridRoom(currentStage.StartRoomTypes[0], new Vector2Int(0, 0));
        //CreateGridRoom(currentStage.OptionalRoomTypes[0], new Vector2Int(1, 0));
        //CreateGridRoom(currentStage.OptionalRoomTypes[0], new Vector2Int(0, 1));
        //CreateGridRoom(currentStage.OptionalRoomTypes[0], new Vector2Int(0, -1));
        //CreateGridRoom(currentStage.OptionalRoomTypes[0], new Vector2Int(-1, 0));
        //CreateGridRoom(currentStage.OptionalRoomTypes[0], new Vector2Int(2, 0));
        //routeController = GetComponent<DungeonRouteController>();

        routeController.InitializeGenerator(currentStage);

        List<Vector2Int> poses = routeController.SelectedGridPositions;

        CreateGridRoom(currentStage.StartRoomTypes[0], poses[0]);

        for (int i = 1; i < poses.Count; i++)
        {
            CreateGridRoom(currentStage.OptionalRoomTypes[0], poses[i]);
        }
    }

    private void Update()
    {
        UpdateRoomQueue();
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
    public void CreateGridRoom(RoomTypeData roomTypeData, Vector2Int gridPosition)
    {
        //if (DoesPositionTaken(gridPosition))
        //    return;

        //takenGridPositions.Add(gridPosition);

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
    public void InitializeStageRoom()
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
    /// Занята ли позиция в сетке подземелья?
    /// </summary>
    private bool DoesPositionTaken(Vector2Int gridPosition)
    {
        return takenGridPositions.Exists(pos => pos == gridPosition);
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
