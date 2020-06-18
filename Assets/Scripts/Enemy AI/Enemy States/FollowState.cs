using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : BaseState
{
    /// <summary>
    /// Обладатель текущего состояния.
    /// </summary>
    private Enemy enemy;
    /// <summary>
    /// Определяет, куда направлен взгляд врага.
    /// </summary>
    private bool FacingRight = true;

    public FollowState(Enemy enemy) : base(enemy.gameObject, false)
    {
        this.enemy = enemy;
    }

    public override Type Handle()
    {
        if (!enemy.IsTargetInRange(enemy.ViewDistance))
        {
            enemy.Animator.SetBool("IsWalking", false);
            return typeof(PatrolState);
        }

        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, enemy.Target.position, enemy.MoveSpeed * Time.deltaTime);

        float relativePosX = enemy.Target.position.x - enemy.transform.position.x;

        if (relativePosX < 0 && FacingRight || relativePosX > 0 && !FacingRight)
        {
            FaceTarget();
        }

        if (enemy.IsTargetInRange(enemy.AttackDistance))
        {
            enemy.Animator.SetBool("IsWalking", false);
            return typeof(AttackState);
        }

        return null;
    }
    /// <summary>
    /// Повернуть спрайт в сторону цели.
    /// </summary>
    private void FaceTarget()
    {
        FacingRight = !FacingRight;

        Vector3 flipLocalScale = enemy.transform.localScale;
        flipLocalScale.x *= -1;
        enemy.transform.localScale = flipLocalScale;
    }
}
