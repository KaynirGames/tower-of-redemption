using KaynirGames.AI;
using KaynirGames.Movement;
using System.Collections;
using UnityEngine;

public enum EnemyStateKey
{
    PlayerInSight,
    PlayerOffSight,
    TargetIsLost,
    TargetIsReached
}

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _sightRange = 5f;
    [SerializeField] private float _triggerBattleRange = 1f;
    [SerializeField] private bool _includeObstaclesOnPatrol = false;
    [SerializeField] private bool _displayGizmos = true;
    [SerializeField] private BaseMovement _movementMechanics = null;

    public bool DestinationReached => _movementMechanics.ReachedDestination;

    private StateMachine<EnemyStateKey> _stateMachine;
    private Transform _target;

    private bool _isWaitingDone = true;
    private Coroutine _waitingRoutine;

    private void Start()
    {
        _target = GameMaster.Instance.Player.transform;

        var enemyWait = new EnemyWait(this, 3f);
        var enemyBattle = new EnemyBattle(this);
        var enemyChase = new EnemyChase(this, _target);
        var enemyPatrol = new EnemyPatrol(this, _includeObstaclesOnPatrol, enemyWait);

        // Переходы из состояния патрулирования.
        enemyPatrol.AddTransition(EnemyStateKey.PlayerInSight, enemyChase);

        // Переходы из состояния преследования цели.
        enemyChase.AddTransition(EnemyStateKey.TargetIsLost, enemyWait);
        enemyChase.AddTransition(EnemyStateKey.TargetIsReached, enemyBattle);

        // Переходы из состояния ожидания.
        enemyWait.AddTransition(EnemyStateKey.PlayerInSight, enemyChase);
        enemyWait.AddTransition(EnemyStateKey.PlayerOffSight, enemyPatrol);

        _stateMachine = new StateMachine<EnemyStateKey>(enemyWait);
    }

    private void Update()
    {
        _stateMachine.Update();

        if (TargetInRange(_target, _sightRange))
        {
            if (TargetInRange(_target, _triggerBattleRange))
            {
                GameMaster.Instance.IsBattle = true;
                _stateMachine.TransitionNext(EnemyStateKey.TargetIsReached);
            }
            else
            {
                _stateMachine.TransitionNext(EnemyStateKey.PlayerInSight);
            }
        }
        else
        {
            if (_isWaitingDone)
            {
                _stateMachine.TransitionNext(EnemyStateKey.PlayerOffSight);
            }
            else
            {
                _stateMachine.TransitionNext(EnemyStateKey.TargetIsLost);
            }
        }
    }

    public void SetDestination(Vector2 targetPosition)
    {
        _movementMechanics.SetMovementPosition(targetPosition);
    }

    public void Wait(float waitingTime)
    {
        if (_waitingRoutine != null) StopCoroutine(_waitingRoutine);
        _waitingRoutine = StartCoroutine(WaitForDelay(waitingTime));
    }

    private IEnumerator WaitForDelay(float delay)
    {
        _isWaitingDone = false;
        yield return new WaitForSeconds(delay);
        _isWaitingDone = true;
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
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _triggerBattleRange);
        }
    }
}