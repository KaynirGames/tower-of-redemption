using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float followSpeed = 100f;
    [SerializeField] private RoomRuntimeSet loadedStageRooms = null;

    private Room activeRoom;
    private bool isChangingRoom;

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (!isChangingRoom)
            return;

        activeRoom = loadedStageRooms.Items.Find(room => room.IsActiveRoom);

        if (activeRoom == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, GetTargetPosition(), followSpeed * Time.deltaTime);

        if (transform.position == GetTargetPosition())
        {
            isChangingRoom = false;
        }
    }

    public void OnActiveRoomChanged()
    {
        isChangingRoom = true;
    }

    private Vector3 GetTargetPosition()
    {
        if (activeRoom == null)
        {
            return Vector3.zero;
        }

        Vector3 targetPos = activeRoom.GetRoomCenter();
        targetPos.z = transform.position.z;

        return targetPos;
    }
}
