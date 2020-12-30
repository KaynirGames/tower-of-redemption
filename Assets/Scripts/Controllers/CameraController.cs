using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _followSpeed = 100f;

    private void Awake()
    {
        Room.OnActiveRoomChange += FollowActiveRoom;
    }

    private void FollowActiveRoom(Room activeRoom)
    {
        StartCoroutine(MoveCameraRoutine(GetRoomPosition(activeRoom)));
    }

    private Vector3 GetRoomPosition(Room activeRoom)
    {
        Vector3 targetPos = activeRoom.transform.position;
        targetPos.z = transform.position.z;

        return targetPos;
    }

    private IEnumerator MoveCameraRoutine(Vector3 position)
    {
        while (transform.position != position)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                                                     position,
                                                     _followSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnDestroy()
    {
        Room.OnActiveRoomChange -= FollowActiveRoom;
    }
}
