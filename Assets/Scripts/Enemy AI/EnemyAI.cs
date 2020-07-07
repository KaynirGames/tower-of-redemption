using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaynirGames.AI;

public enum EnemyStateKey
{
    PlayerInSight,
    PlayerOffSight
}

public class EnemyAI : MonoBehaviour
{
    private StateMachine<EnemyStateKey> _stateMachine;

    private EnemyPatrol _enemyPatrol;
    private EnemyChase _enemyChase;

    private void Start()
    {
        _enemyChase = new EnemyChase();
        _enemyPatrol = new EnemyPatrol();

        _stateMachine = new StateMachine<EnemyStateKey>(_enemyPatrol);
    }
}