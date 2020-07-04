using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : BaseState
{
    private Enemy enemy;

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

        return null;
    }
}
