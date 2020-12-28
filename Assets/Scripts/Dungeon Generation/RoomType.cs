using UnityEngine;

[CreateAssetMenu(fileName = "NewRoomType", menuName = "Scriptable Objects/Dungeon Generation/Room Type")]
public class RoomType : ScriptableObject
{
    [SerializeField] private DoorType _doorType = null;

    public DoorType DoorType => _doorType;
}