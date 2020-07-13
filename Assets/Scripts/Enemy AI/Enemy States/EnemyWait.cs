﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaynirGames.AI;

public class EnemyWait : BaseState<EnemyStateKey>
{
    EnemyAI _enemyAI;
    float _waitingTime;

    public EnemyWait(EnemyAI enemyAI, float waitingTime)
    {
        _enemyAI = enemyAI;
        _waitingTime = waitingTime;
    }

    public override void EnterState()
    {
        Debug.Log("Waiting for my next action.");
        _enemyAI.Wait(_waitingTime);
    }

    public override BaseState<EnemyStateKey> UpdateState()
    {
        return null;
    }

    public override void ExitState()
    {
        
    }
}
