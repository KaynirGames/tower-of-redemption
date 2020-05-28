using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRoomType", menuName = "Dungeon Generation/Room Type")]
public class RoomType : ScriptableObject
{
    /// <summary>
    /// Название типа комнаты
    /// </summary>
    public string Name;
}
