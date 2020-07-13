using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private GameObjectRuntimeSet _doorPrefabSet = null;

    public Player Player { get; set; }

    public GameObjectRuntimeSet DoorPrefabSet => _doorPrefabSet;

    public List<Room> LoadedRooms { get; private set; } = new List<Room>();

    /// <summary>
    /// Для тестирования!
    /// </summary>
    public bool IsBattle { get; set; }

    #region Singleton
    public static GameMaster Instance { get; private set; }

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
    #endregion
}
