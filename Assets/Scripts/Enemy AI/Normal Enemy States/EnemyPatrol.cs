using KaynirGames.AI;
using KaynirGames.Pathfinding;
using UnityEngine;

public class EnemyPatrol : BaseState<EnemyStateKey>
{
    private EnemyAI _enemyAI;
    private BaseState<EnemyStateKey> _nextState;
    private Vector2[] _patrolWaypoints;
    private Vector2 _currentDestination;

    public EnemyPatrol(EnemyAI enemyAI, bool includeObstacles, BaseState<EnemyStateKey> nextState)
    {
        _enemyAI = enemyAI;
        _nextState = nextState;
        _patrolWaypoints = Pathfinder.GetGridWorldPoints(includeObstacles);
    }

    public override void EnterState()
    {
        PatrolTo();
    }

    public override BaseState<EnemyStateKey> UpdateState()
    {
        if (!_enemyAI.MovePosition.IsMoving)
        {
            return _nextState;
        }
        return null;
    }

    public override void ExitState() { }

    private void PatrolTo()
    {
        int index = Random.Range(0, _patrolWaypoints.Length);
        _currentDestination = _patrolWaypoints[index];
        _enemyAI.MovePosition.SetPosition(_currentDestination);
    }
}