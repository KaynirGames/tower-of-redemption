using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDoorType", menuName = "Scriptable Objects/Dungeon Generation/Door Type")]
public class DoorType : ScriptableObject
{
    /// <summary>
    /// Приоритет размещения типа двери в проходах между комнатами.
    /// </summary>
    public int PlacingPriority = 0;
    /// <summary>
    /// Требуется ли ключ для открытия дверей данного типа?
    /// </summary>
    public bool IsRequireKey;
    /// <summary>
    /// Ключ, открывающий двери данного типа (если требуется).
    /// </summary>
    public GameObject RequiredKey;
}
