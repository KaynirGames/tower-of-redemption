using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }

    [SerializeField] private GameObjectRuntimeSet _doorPrefabSet = null;

    public Player ActivePlayer { get; private set; }

    public GameObjectRuntimeSet DoorPrefabSet => _doorPrefabSet;

    public List<Room> LoadedRooms { get; private set; } = new List<Room>();

    public bool IsPause { get; private set; }

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

        Player.OnPlayerActive += SetActivePlayer;
    }


    public void TogglePause()
    {
        Time.timeScale = IsPause ? 1f : 0f;
        IsPause = !IsPause;
    }

    private void SetActivePlayer(Player player) => ActivePlayer = player;
}
