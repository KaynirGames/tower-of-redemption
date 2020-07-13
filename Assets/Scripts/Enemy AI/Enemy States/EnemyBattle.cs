using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaynirGames.AI;

public class EnemyBattle : BaseState<EnemyStateKey>
{
    private EnemyAI _enemyAI;

    public EnemyBattle(EnemyAI enemyAI)
    {
        _enemyAI = enemyAI;
    }

    public override void EnterState()
    {
        Debug.Log("Battle system is active!");
    }

    public override BaseState<EnemyStateKey> UpdateState()
    {
        return null;
    }

    public override void ExitState()
    {
        
    }
}
