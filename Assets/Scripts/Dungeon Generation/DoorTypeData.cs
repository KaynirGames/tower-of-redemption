using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType { Default, Boss, Shop, Secret }

[CreateAssetMenu(fileName = "NewDoorTypeData", menuName = "Scriptable Objects/Dungeon Generation/Door Type Data")]
public class DoorTypeData : ScriptableObject
{
    public DoorType DoorType = DoorType.Default;

    public bool IsRequireKey;

    public GameObject RequiredKey;
}
