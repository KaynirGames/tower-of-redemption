using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private Enemy enemy;

    private float nextAttackTime = 0;

    public AttackState(Enemy enemy) : base(enemy.gameObject, false)
    {
        this.enemy = enemy;
    }

    public override Type Handle()
    {
        if (nextAttackTime <= 0)
        {
            enemy.Animator.SetTrigger("Attack");
            enemy.AttackIsComplete = false;
            nextAttackTime = enemy.AttackCooldown;
        }
        else
        {
            nextAttackTime -= Time.deltaTime;
        }

        if (!enemy.IsTargetInRange(enemy.AttackDistance)
            && enemy.AttackIsComplete)
        {
            enemy.Animator.SetBool("IsWalking", true);
            return typeof(FollowState);
        }

        return null;
    }
}
