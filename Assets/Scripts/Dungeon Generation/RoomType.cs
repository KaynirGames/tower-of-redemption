using UnityEngine;

[CreateAssetMenu(fileName = "NewRoomType", menuName = "Scriptable Objects/Dungeon Generation/Room Type")]
public class RoomType : ScriptableObject
{
    [SerializeField] private string _roomSceneName = "UndefinedRoom";
    [SerializeField] private DoorType _doorType = null;
    [SerializeField] private bool _isStartPoint = false;

    public string RoomSceneName => _roomSceneName;
    public DoorType DoorType => _doorType;
    public bool IsStartPoint => _isStartPoint;
}