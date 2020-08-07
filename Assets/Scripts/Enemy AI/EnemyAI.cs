using KaynirGames.AI;
using KaynirGames.Movement;
using UnityEngine;

/// <summary>
/// Ключи перехода в состояния.
/// </summary>
public enum EnemyStateKey
{
    PlayerInSight,
    PlayerOffSight,
    PlayerInBattle,
    WaitComplete
}

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _sightRange = 5f; // Радиус обзора.
    [SerializeField] private float _waitTimeOnTargetLost = 3f; // Время ожидания при потере цели преследования.
    [SerializeField] private bool _includeObstaclesOnPatrol = false; // Использовать точки с препятствиями при патрулировании.
    [SerializeField] private bool _displayGizmos = true; // Отображение гизмо объектов.
    [SerializeField] private BaseMovement _movementMechanics = null; // Механика перемещения.
    /// <summary>
    /// Противник перемещается в настоящий момент?
    /// </summary>
    public bool IsMoving => _movementMechanics.IsMoving;

    private StateMachine<EnemyStateKey> _stateMachine; // Конечный автомат состояний ИИ противника.
    private Transform _target; // Цель ИИ противника.

    private void Start()
    {
        _target = GameMaster.Instance.ActivePlayer.transform;

        EnemyWait enemyWait = new EnemyWait(this, _waitTimeOnTargetLost);
        //EnemyBattle enemyBattle = new EnemyBattle(this);
        EnemyChase enemyChase = new EnemyChase(this, _target);
        EnemyPatrol enemyPatrol = new EnemyPatrol(this, _includeObstaclesOnPatrol, enemyWait);

        // Переходы из состояния патрулирования.
        enemyPatrol.AddTransition(EnemyStateKey.PlayerInSight, enemyChase);

        // Переходы из состояния преследования цели.
        enemyChase.AddTransition(EnemyStateKey.PlayerOffSight, enemyWait);
        //enemyChase.AddTransition(EnemyStateKey.PlayerInBattle, enemyBattle);

        // Переходы из состояния ожидания.
        enemyWait.AddTransition(EnemyStateKey.PlayerInSight, enemyChase);
        enemyWait.AddTransition(EnemyStateKey.WaitComplete, enemyPatrol);

        _stateMachine = new StateMachine<EnemyStateKey>(enemyWait);
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
    /// <summary>
    /// Выставить позицию для перемещения.
    /// </summary>
    public void SetDestination(Vector2 targetPosition)
    {
        _movementMechanics.SetMovementPosition(targetPosition);
    }
    /// <summary>
    /// Остановить перемещение.
    /// </summary>
    public void StopMovement()
    {
        _movementMechanics.StopMovement();
    }
    /// <summary>
    /// Выставить ключ перехода в другое состояние.
    /// </summary>
    public void SetTransition(EnemyStateKey transitionKey)
    {
        _stateMachine.TransitionNext(transitionKey);
    }
    /// <summary>
    /// Проверить, находится ли цель на заданной дистанции.
    /// </summary>
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