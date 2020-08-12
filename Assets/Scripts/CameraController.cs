using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float followSpeed = 100f; // Скорость следования камеры.

    private void Awake()
    {
        Room.OnActiveRoomChange += MoveCamera;
    }
    /// <summary>
    /// Переместить камеру в активную комнату.
    /// </summary>
    public void MoveCamera(Room activeRoom)
    {
        StartCoroutine(UpdatePosition(GetNewPosition(activeRoom)));
    }
    /// <summary>
    /// Перемещает камеру в новую позицию.
    /// </summary>
    private IEnumerator UpdatePosition(Vector3 newPosition)
    {
        while (transform.position != newPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, followSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
    /// <summary>
    /// Получить новую позицию для камеры.
    /// </summary>
    private Vector3 GetNewPosition(Room activeRoom)
    {
        Vector3 targetPos = activeRoom.transform.position;
        targetPos.z = transform.position.z;

        return targetPos;
    }
}
