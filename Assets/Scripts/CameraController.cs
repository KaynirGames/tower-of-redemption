using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float followSpeed = 100f; // Скорость следования камеры.
    [SerializeField] private RoomRuntimeSet activeRoom = null; // Активная комната, где находится игрок.

    private bool isChangingRoom;

    private void Update()
    {
        UpdatePosition();
    }
    /// <summary>
    /// Обновляет позицию камеры при смене активной комнаты.
    /// </summary>
    private void UpdatePosition()
    {
        if (!isChangingRoom || activeRoom.Count == 0)
            return;

        Vector3 targetPos = GetTargetPosition(activeRoom.GetObject(0));

        transform.position = Vector3.MoveTowards(transform.position, targetPos, followSpeed * Time.deltaTime);

        if (transform.position == targetPos)
        {
            isChangingRoom = false;
        }
    }
    /// <summary>
    /// Отклик на событие при смене активной комнаты.
    /// </summary>
    public void OnActiveRoomChanged()
    {
        isChangingRoom = true;
    }
    /// <summary>
    /// Получить новую целевую позицию камеры в активной комнате.
    /// </summary>
    /// <param name="activeRoom">Активная комната.</param>
    /// <returns></returns>
    private Vector3 GetTargetPosition(Room activeRoom)
    {
        Vector3 targetPos = activeRoom.GetWorldPosition();
        targetPos.z = transform.position.z;

        return targetPos;
    }
}
