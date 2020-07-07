﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaynirGames.AI;

public class EnemyPatrol : BaseState<EnemyStateKey>
{
    public override void EnterState()
    {
        Debug.Log("Begin patrol.");
    }

    public override BaseState<EnemyStateKey> UpdateState()
    {
        Debug.Log("Stop patrol.");
        return null;
    }

    public override void ExitState()
    {
        Debug.Log("Patrolling...");
    }
}
