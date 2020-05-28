using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomData
{
    public string Name { get; set; }
    public Vector2Int DungeonGridPosition { get; set; }
}

public class DungeonStageManager : MonoBehaviour
{
    [SerializeField] private DungeonStage currentStage = null; // Информация о текущем этаже подземелья.

    public RoomType[] roomTypes;

    /// <summary>
    /// Список загруженных комнат на уровне подземелья.
    /// </summary>
    private List<Room> loadedRooms = new List<Room>();

    private Queue<RoomData> loadRoomQueue = new Queue<RoomData>();
    
    private RoomData currentLoadRoomData;

    private bool isLoadingRoom;

    private void Start()
    {
        CreateDungeonRoom(roomTypes[0], new Vector2Int(0, 0));
        CreateDungeonRoom(roomTypes[0], new Vector2Int(1, 0));
        CreateDungeonRoom(roomTypes[0], new Vector2Int(0, 1));
    }

    private void Update()
    {
        UpdateRoomQueue();
    }

    public void UpdateRoomQueue()
    {
        if (isLoadingRoom || loadRoomQueue.Count == 0)
            return;

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomCoroutine(currentLoadRoomData));
    }

    public void CreateDungeonRoom(RoomType roomType, Vector2Int gridPosition)
    {
        if (DoesRoomExist(gridPosition))
            return;

        RoomData newRoomData = new RoomData();
        newRoomData.Name = $"{currentStage.Name}_{roomType.Name}";
        newRoomData.DungeonGridPosition = gridPosition;

        loadRoomQueue.Enqueue(newRoomData);
    }

    public void InitializeDungeonRoom(Room room)
    {
        room.LocalGridPosition = currentLoadRoomData.DungeonGridPosition;
        room.SetWorldPosition();

        isLoadingRoom = false;

        loadedRooms.Add(room);
    }

    public void RegisterRoom()
    {
        Debug.Log("Комната загрузилась!");
    }

    /// <summary>
    /// Существует ли комната в указанной точке на сетке подземелья?
    /// </summary>
    public bool DoesRoomExist(Vector2Int gridPosition)
    {
        return loadedRooms.Find(loadedRoom => loadedRoom.LocalGridPosition == gridPosition) != null;  
    }

    /// <summary>
    /// Асинхронная загрузка сцены с комнатой поверх основной сцены с подземельем.
    /// </summary>
    /// <param name="roomSceneName">Название сцены с комнатой.</param>
    /// <returns></returns>
    private IEnumerator LoadRoomCoroutine(RoomData roomData)
    {
        AsyncOperation roomLoadAsync = SceneManager.LoadSceneAsync(roomData.Name, LoadSceneMode.Additive);

        while (!roomLoadAsync.isDone)
        {
            yield return null;
        }
    }
}
