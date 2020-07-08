using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaynirGames.AI;

public class EnemyPatrol : BaseState<EnemyStateKey>
{
    EnemyAI _enemyAI;

    public EnemyPatrol(EnemyAI enemyAI)
    {
        _enemyAI = enemyAI;
    }

    public override void EnterState()
    {
        
    }

    public override BaseState<EnemyStateKey> UpdateState()
    {
        
        return null;
    }

    public override void ExitState()
    {
        
    }
}