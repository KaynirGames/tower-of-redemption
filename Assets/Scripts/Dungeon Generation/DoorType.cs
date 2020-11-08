using UnityEngine;

[CreateAssetMenu(fileName = "NewDoor_Stage", menuName = "Scriptable Objects/Dungeon Generation/Door Type")]
public class DoorType : ScriptableObject
{
    [SerializeField] private int _placingPriority = 0;
    [SerializeField] private GameObject _doorGFX = null;

    public int PlacingPriority => _placingPriority;
    public GameObject DoorGFX => _doorGFX;
}
