using UnityEngine;

[CreateAssetMenu(fileName = "NewDoorType", menuName = "Scriptable Objects/Dungeon Generation/Door Type")]
public class DoorType : ScriptableObject
{
    [SerializeField] private int _placingPriority = 0;

    public int PlacingPriority => _placingPriority;
}
