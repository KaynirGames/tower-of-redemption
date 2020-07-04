using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : BaseState
{
    /// <summary>
    /// Обладатель текущего состояния.
    /// </summary>
    private Enemy _enemy;

    public EnemyFollow(Enemy enemy) : base(enemy.gameObject, false)
    {
        _enemy = enemy;
    }

    public override Type Handle()
    {
        if (!_enemy.IsTargetInRange(_enemy.Spec.ViewDistance))
        {
            _enemy.Animator.SetBool("IsWalking", true);
            return typeof(EnemyPatrol);
        }

        if (_enemy.IsTargetInRange(_enemy.Spec.AttackDistance))
        {
            _enemy.Animator.SetBool("IsWalking", false);
            return typeof(EnemyAttack);
        }

        return null;
    }
}
