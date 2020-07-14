using KaynirGames.AI;
using KaynirGames.Movement;
using System.Collections;
using UnityEngine;

/// <summary>
/// Ключи перехода в состояния.
/// </summary>
public enum EnemyStateKey
{
    PlayerInSight,
    PlayerOffSight,
    PlayerInBattle,
    WaitCompleted
}

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _sightRange = 5f; // Радиус обзора.
    [SerializeField] private bool _includeObstaclesOnPatrol = false; // Использовать точки с препятствиями при патрулировании.
    [SerializeField] private bool _displayGizmos = true; // Отображение гизмо объектов.
    [SerializeField] private BaseMovement _movementMechanics = null; // Механика перемещения.

    /// <summary>
    /// Противник перемещается в настоящий момент?
    /// </summary>
    public bool IsMoving => _movementMechanics.IsMoving;

    private StateMachine<EnemyStateKey> _stateMachine; // Конечный автомат состояний ИИ противника.
    private Transform _target; // Цель ИИ противника.
    private bool _isWaiting; // Нахождение ИИ в состоянии бездействия.

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
        enemyChase.AddTransition(EnemyStateKey.PlayerOffSight, enemyWait);
        enemyChase.AddTransition(EnemyStateKey.PlayerInBattle, enemyBattle);

        // Переходы из состояния ожидания.
        enemyWait.AddTransition(EnemyStateKey.PlayerInSight, enemyChase);
        enemyWait.AddTransition(EnemyStateKey.WaitCompleted, enemyPatrol);

        _stateMachine = new StateMachine<EnemyStateKey>(enemyWait);
    }

    private void Update()
    {
        _stateMachine.Update();

        if (TargetInRange(_target, _sightRange))
        {
            if (BattleSystem.Instance.IsBattleActive)
            {
                _stateMachine.TransitionNext(EnemyStateKey.PlayerInBattle);
            }
            else
            {
                _stateMachine.TransitionNext(EnemyStateKey.PlayerInSight);
            }
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
    /// Бездействовать заданное время.
    /// </summary>
    public void Wait(float waitingTime)
    {
        if (_isWaiting) return;

        _isWaiting = true;
        StartCoroutine(WaitForDelay(waitingTime));
    }
    /// <summary>
    /// Корутина бездействия.
    /// </summary>
    private IEnumerator WaitForDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _stateMachine.TransitionNext(EnemyStateKey.WaitCompleted);
        _isWaiting = false;
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