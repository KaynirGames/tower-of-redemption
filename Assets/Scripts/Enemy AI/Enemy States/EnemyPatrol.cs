using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaynirGames.AI;

public class EnemyPatrol : BaseState<EnemyStateKey>
{
    private EnemyAI _enemyAI;
    private Vector2[] _patrolWaypoints;
    private Vector2 _currentDestination;

    public EnemyPatrol(EnemyAI enemyAI, bool bypassObstacles)
    {
        _enemyAI = enemyAI;
        // Задать точки патрулирования.
    }

    public override void EnterState()
    {
        PatrolNext();
    }

    public override BaseState<EnemyStateKey> UpdateState()
    {
        if (Vector2.Distance(_enemyAI.transform.position, _currentDestination) <= 0.02f)
        {
            PatrolNext();
        }
        return null;
    }

    public override void ExitState()
    {
        
    }

    private void PatrolNext()
    {
        int index = Random.Range(0, _patrolWaypoints.Length);
        _currentDestination = _patrolWaypoints[index];
        _enemyAI.SetDestination(_currentDestination);
    }
}