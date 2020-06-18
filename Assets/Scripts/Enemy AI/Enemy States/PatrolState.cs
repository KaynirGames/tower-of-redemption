using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    private Enemy enemy;

    public PatrolState(Enemy enemy) : base(enemy.gameObject, true)
    {
        this.enemy = enemy;
    }

    public override Type Handle()
    {
        if (enemy.IsTargetInRange(enemy.ViewDistance))
        {
            enemy.Animator.SetBool("IsWalking", true);
            return typeof(FollowState);
        }
      
        Debug.Log("Патрулирую.");
        return null;
    }
}
