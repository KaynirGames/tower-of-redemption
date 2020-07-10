using KaynirGames.AI;
using KaynirGames.Movement;
using UnityEngine;

public enum EnemyStateKey
{
    PlayerInSight,
    PlayerOffSight
}

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _sightRange = 5f;
    [SerializeField] private bool _bypassObstaclesOnPatrol = true;
    [SerializeField] private bool _displayGizmos = true;
    [SerializeField] private BaseMovement _movementMechanics = null;

    private StateMachine<EnemyStateKey> _stateMachine;
    private Transform _target;

    private void Start()
    {
        _target = GameMaster.Instance.Player.transform;

        var enemyChase = new EnemyChase(this, _target);
        var enemyPatrol = new EnemyPatrol(this, _bypassObstaclesOnPatrol);

        // Переходы из состояния патрулирования.
        enemyPatrol.AddTransition(EnemyStateKey.PlayerInSight, enemyChase);

        // Переходы из состояния преследования цели.
        enemyChase.AddTransition(EnemyStateKey.PlayerOffSight, enemyPatrol);

        _stateMachine = new StateMachine<EnemyStateKey>(enemyPatrol);
    }

    private void Update()
    {
        _stateMachine.Update();

        if (TargetInRange(_target, _sightRange))
        {
            _stateMachine.TransitionNext(EnemyStateKey.PlayerInSight);
        }
        else
        {
            _stateMachine.TransitionNext(EnemyStateKey.PlayerOffSight);
        }
    }

    public void SetDestination(Vector2 targetPosition)
    {
        _movementMechanics.SetMovementPosition(targetPosition);
    }

    private bool TargetInRange(Transform target, float range)
    {
        return Vector2.Distance(transform.position, target.position) <= range;
    }

    private void OnDrawGizmos()
    {
        if (_displayGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _sightRange);
        }
    }
}