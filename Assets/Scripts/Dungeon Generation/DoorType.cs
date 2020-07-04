using UnityEngine;

[CreateAssetMenu(fileName = "NewDoorType", menuName = "Scriptable Objects/Dungeon Generation/Door Type")]
public class DoorType : ScriptableObject
{
    [SerializeField] private int _placingPriority = 0;
    [SerializeField] private bool _needKey = false;
    [SerializeField] private GameObject _requiredKey = null;
    /// <summary>
    /// Приоритет размещения типа двери в проходах между комнатами.
    /// </summary>
    public int PlacingPriority => _placingPriority;
    /// <summary>
    /// Требуется ли ключ для открытия дверей данного типа?
    /// </summary>
    public bool NeedKey => _needKey;
    /// <summary>
    /// Ключ, открывающий двери данного типа (если требуется).
    /// </summary>
    public GameObject RequiredKey => _requiredKey;
}
