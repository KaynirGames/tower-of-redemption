using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : BaseState
{
    /// <summary>
    /// Обладатель текущего состояния.
    /// </summary>
    private Enemy enemy;

    public EnemyFollow(Enemy enemy) : base(enemy.gameObject, false)
    {
        this.enemy = enemy;
    }

    public override Type Handle()
    {
        if (!enemy.IsTargetInRange(enemy.Spec.ViewDistance))
        {
            enemy.Animator.SetBool("IsWalking", true);
            return typeof(EnemyPatrol);
        }

        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, enemy.Target.position, enemy.Spec.MoveSpeed * Time.deltaTime);

        enemy.FaceTarget(enemy.Target.position);

        if (enemy.IsTargetInRange(enemy.Spec.AttackDistance))
        {
            enemy.Animator.SetBool("IsWalking", false);
            return typeof(EnemyAttack);
        }

        return null;
    }
}
