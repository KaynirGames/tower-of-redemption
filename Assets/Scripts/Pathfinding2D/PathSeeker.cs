using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaynirGames.Pathfinding2D;

public class PathSeeker : MonoBehaviour
{
    public Transform target;
    public PathfindingManager pathfindingManager;

    private Vector2[] currentPath;
    private float currentMoveSpeed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RequestPath(transform.position, target.position, 2f);
        }
    }

    public void RequestPath(Vector2 startPoint, Vector2 endPoint, float moveSpeed)
    {
        pathfindingManager.AddPathRequest(startPoint, endPoint, ReceivePath);
        currentMoveSpeed = moveSpeed;
    }

    private void ReceivePath(Vector2[] waypoints, bool success)
    {       
        if (success)
        {
            Debug.Log("Путь успешно передан искателю!");
            currentPath = waypoints;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
        else
        {
            Debug.Log("Путь заблокирован!");
        }
    }

    private IEnumerator FollowPath()
    {
        int waypointIndex = 0;
        Vector2 currentWaypoint = currentPath[waypointIndex];

        while (true)
        {
            if (Vector2.Distance(transform.position, currentWaypoint) <= 0.1f)
            {
                waypointIndex++;

                if (waypointIndex == currentPath.Length)
                    yield break;

                currentWaypoint = currentPath[waypointIndex];
            }
            transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, currentMoveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        if (currentPath != null)
        {
            Gizmos.color = Color.green;
            
            for (int i = 1; i < currentPath.Length; i++)
            {
                Gizmos.DrawLine(currentPath[i-1], currentPath[i]);
            }
        }
    }
}
