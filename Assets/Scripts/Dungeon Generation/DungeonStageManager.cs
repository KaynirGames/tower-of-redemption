using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonStageManager : MonoBehaviour
{
    [SerializeField] private DungeonStage currentStage = null; // Информация о текущем этаже подземелья.
    [SerializeField] private RoomRuntimeSet loadedStageRooms = null; // Набор комнат, загруженных на этаже подземелья.

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

    private void Start()
    {
        CreateGridRoom(currentStage.StartRoomType, new Vector2Int(0, 0));
        CreateGridRoom(currentStage.FillerRoomTypes[0], new Vector2Int(1, 0));
        CreateGridRoom(currentStage.FillerRoomTypes[0], new Vector2Int(0, 1));
        CreateGridRoom(currentStage.FillerRoomTypes[0], new Vector2Int(0, -1));
        CreateGridRoom(currentStage.FillerRoomTypes[0], new Vector2Int(-1, 0));
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
    /// <param name="roomType">Тип комнаты.</param>
    /// <param name="gridPosition">Позиция на сетке координат подземелья.</param>
    public void CreateGridRoom(RoomType roomType, Vector2Int gridPosition)
    {
        if (DoesRoomExist(gridPosition))
            return;

        GridRoom newGridRoom = new GridRoom
        {
            RoomSceneName = $"{currentStage.Name}_{roomType.Name}",
            GridPosition = gridPosition
        };

        gridRoomQueue.Enqueue(newGridRoom);
    }
    /// <summary>
    /// Присвоить комнате место на этаже подземелья (отклик на событие по окончанию загрузки комнаты).
    /// </summary>
    public void InitializeStageRoom()
    {
        if (loadedStageRooms.Items.Count == 0)
        {
            Debug.Log("Набор загруженных комнат пуст!");
            return;
        }

        Room currentRoom = loadedStageRooms.Items.Find(room => !room.IsInitialized);

        currentRoom.DungeonGridPosition = currentGridRoom.GridPosition;
        currentRoom.SetWorldPosition();
        currentRoom.IsInitialized = true;

        isLoadingRoom = false;
    }
    /// <summary>
    /// Существует ли комната на сетке подземелья?
    /// </summary>
    private bool DoesRoomExist(Vector2Int gridPosition)
    {
        return loadedStageRooms.Items.Find(loadedRoom => loadedRoom.DungeonGridPosition == gridPosition) != null;
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
