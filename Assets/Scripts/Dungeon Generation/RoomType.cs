using UnityEngine;

[CreateAssetMenu(fileName = "NewRoomType", menuName = "Scriptable Objects/Dungeon Generation/Room Type")]
public class RoomType : ScriptableObject
{
    /// <summary>
    /// Наименование типа комнаты (также используется для загрузки нужной сцены).
    /// </summary>
    public string Name = "Undefined";
    /// <summary>
    /// Тип двери, относящийся к данному типу комнаты.
    /// </summary>
    public DoorType DoorType;
    /// <summary>
    /// Является ли точкой старта на этаже подземелья?
    /// </summary>
    public bool IsStartingPoint;
}