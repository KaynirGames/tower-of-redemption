using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }

    [SerializeField] private GameObjectRuntimeSet _doorPrefabSet = null;

    public Player Player { get; set; }

    public GameObjectRuntimeSet DoorPrefabSet => _doorPrefabSet;

    public List<Room> LoadedRooms { get; private set; } = new List<Room>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
