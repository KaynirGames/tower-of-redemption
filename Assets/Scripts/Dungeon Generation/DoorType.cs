using UnityEngine;

[CreateAssetMenu(fileName = "NewDoorType", menuName = "Scriptable Objects/Dungeon Generation/Door Type")]
public class DoorType : ScriptableObject
{
    [SerializeField] private int _placingPriority = 0;
    /// <summary>
    /// Приоритет размещения типа двери в проходах между комнатами.
    /// </summary>
    public int PlacingPriority => _placingPriority;
}
