using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : BaseState
{
    private Enemy enemy;

    public EnemyAttack(Enemy enemy) : base(enemy.gameObject, false)
    {
        this.enemy = enemy;
    }

    public override Type Handle()
    {
        enemy.TryAttack();

        if (!enemy.IsTargetInRange(enemy.Spec.AttackDistance)
            && enemy.CanMove)
        {
            enemy.Animator.SetBool("IsWalking", true);
            return typeof(EnemyFollow);
        }

        return null;
    }
}
