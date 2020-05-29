using UnityEngine;

[CreateAssetMenu(fileName = "NewRoomType", menuName = "Scriptable Objects/Dungeon Generation/Room Type")]
public class RoomType : ScriptableObject
{
    /// <summary>
    /// Название типа комнаты
    /// </summary>
    public string Name;
}
