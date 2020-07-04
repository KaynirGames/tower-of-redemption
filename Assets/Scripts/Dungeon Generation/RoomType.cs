﻿using UnityEngine;

[CreateAssetMenu(fileName = "NewRoomType", menuName = "Scriptable Objects/Dungeon Generation/Room Type")]
public class RoomType : ScriptableObject
{
    [SerializeField] private string _name = "UndefinedRoom";
    [SerializeField] private DoorType _doorType = null;
    [SerializeField] private bool _isStartPoint = false;
    /// <summary>
    /// Наименование типа комнаты (также используется для загрузки нужной сцены).
    /// </summary>
    public string Name => _name;
    /// <summary>
    /// Тип двери, относящийся к данному типу комнаты.
    /// </summary>
    public DoorType DoorType => _doorType;
    /// <summary>
    /// Является ли точкой старта на этаже подземелья?
    /// </summary>
    public bool IsStartPoint => _isStartPoint;
}