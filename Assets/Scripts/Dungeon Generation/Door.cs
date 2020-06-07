using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Up, Right, Down, Left }

public class Door : MonoBehaviour
{
    [SerializeField] private Direction direction = Direction.Up;
    [SerializeField] private DoorTypeData doorTypeData = null;

    public Direction Direction { get => direction; set => direction = value; }
    public DoorTypeData DoorTypeData => doorTypeData;
}
