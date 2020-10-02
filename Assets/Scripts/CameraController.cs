using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float followSpeed = 100f;

    private void Awake()
    {
        Room.OnActiveRoomChange += MoveCamera;
    }

    public void MoveCamera(Room activeRoom)
    {
        StartCoroutine(UpdatePosition(GetNewPosition(activeRoom)));
    }

    public void MoveCamera(Vector3 position, bool instant)
    {
        if (instant)
        {
            transform.position = position;
        }
        else
        {
            StartCoroutine(UpdatePosition(position));
        }
    }

    private IEnumerator UpdatePosition(Vector3 newPosition)
    {
        while (transform.position != newPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, followSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    private Vector3 GetNewPosition(Room activeRoom)
    {
        Vector3 targetPos = activeRoom.transform.position;
        targetPos.z = transform.position.z;

        return targetPos;
    }
}
