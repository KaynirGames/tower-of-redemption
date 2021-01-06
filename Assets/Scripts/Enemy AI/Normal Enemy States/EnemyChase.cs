using KaynirGames.AI;
using UnityEngine;

public class EnemyChase : BaseState<EnemyStateKey>
{
    private EnemyAI _enemyAI;
    private Transform _target;
    private EnemyCharacter _enemy;

    private Vector2 _previousTargetPosition;

    public EnemyChase(EnemyAI enemyAI, Transform target)
    {
        _enemyAI = enemyAI;
        _target = target;
        _enemy = _enemyAI.GetComponent<EnemyCharacter>();
    }

    public override void EnterState()
    {
        _enemy.Sounds.PlaySoundOneShot("EnemyChase");
        ChaseTarget();
    }

    public override BaseState<EnemyStateKey> UpdateState()
    {
        if (Vector2.Distance(_target.position, _previousTargetPosition) >= 1.5f)
        {
            ChaseTarget();
        }

        return null;
    }

    public override void ExitState() { }

    private void ChaseTarget()
    {
        _previousTargetPosition = _target.position;
        _enemyAI.Enemy.HandleMovement(_previousTargetPosition);
    }
}
