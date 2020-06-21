using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : BaseState
{
    private Enemy enemy;
    private LayerMask obstaclesLayer = LayerMask.GetMask("Obstacle", "Wall");
    private float rayDistance = 5f;
    private Vector2 destination = Vector2.zero;
    private bool hasReachDestination = false;

    public EnemyPatrol(Enemy enemy) : base(enemy.gameObject, true)
    {
        this.enemy = enemy;
    }

    public override Type Handle()
    {
        if (enemy.IsTargetInRange(enemy.Spec.ViewDistance))
        {
            enemy.Animator.SetBool("IsWalking", true);
            return typeof(EnemyFollow);
        }

        if (hasReachDestination || destination == Vector2.zero)
            GetDestination();

        if (Vector2.Distance(enemy.transform.position, destination) <= 0.5f)
        {
            hasReachDestination = true;
            GetDestination();
        }
        else
        {
            if (CheckObstacles(destination.normalized))
            {
                Debug.Log("Пусть заблокирован!");
                GetDestination();
            }
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position,
                                               destination,
                                               enemy.Spec.MoveSpeed * Time.deltaTime);
        }

        return null;
    }

    private void GetDestination()
    {
        hasReachDestination = false;

        destination = new Vector2(
            enemy.transform.position.x + UnityEngine.Random.Range(-2f, 2f),
            enemy.transform.position.y + UnityEngine.Random.Range(-2f, 2f));

        enemy.FaceTarget(destination);
    }

    private bool CheckObstacles(Vector2 direction)
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(enemy.transform.position, direction, rayDistance, obstaclesLayer);
        return raycastHit ? raycastHit.distance <= 0.5f : false;
    }
}
