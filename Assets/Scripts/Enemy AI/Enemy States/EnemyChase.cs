﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaynirGames.AI;

public class EnemyChase : BaseState<EnemyStateKey>
{
    private EnemyAI _enemyAI;
    private Transform _target;
 
    private Vector2 _previousTargetPosition;

    public EnemyChase(EnemyAI enemyAI, Transform target)
    {
        _enemyAI = enemyAI;
        _target = target;
    }

    public override void EnterState()
    {
        _previousTargetPosition = _target.position;
        _enemyAI.SetDestination(_previousTargetPosition);
    }

    public override BaseState<EnemyStateKey> UpdateState()
    {
        if (Vector2.Distance(_target.position, _previousTargetPosition) >= 1.5f)
        {
            _previousTargetPosition = _target.position;
            _enemyAI.SetDestination(_previousTargetPosition);
        }

        return null;
    }

    public override void ExitState()
    {
        Debug.Log("Target is lost!");
    }
}
