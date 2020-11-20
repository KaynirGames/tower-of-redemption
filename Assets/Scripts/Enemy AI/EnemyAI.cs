using KaynirGames.AI;
using KaynirGames.Movement;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _sightRange = 5f;
    [SerializeField] private float _waitTimeOnTargetLost = 3f;
    [SerializeField] private bool _includeObstaclesOnPatrol = false;
    [SerializeField] private bool _displayGizmos = true;
    
    public MovePositionBase MovePosition { get; private set; }

    private StateMachine<EnemyStateKey> _stateMachine;
    private Transform _target;

    private void Awake()
    {
        MovePosition = GetComponent<MovePositionBase>();
    }

    private void Start()
    {
        _target = PlayerCharacter.Active.transform;

        EnemyWait enemyWait = new EnemyWait(this, _waitTimeOnTargetLost);
        EnemyChase enemyChase = new EnemyChase(this, _target);
        EnemyPatrol enemyPatrol = new EnemyPatrol(this, _includeObstaclesOnPatrol, enemyWait);

        // Переходы из состояния патрулирования.
        enemyPatrol.AddTransition(EnemyStateKey.PlayerInSight, enemyChase);

        // Переходы из состояния преследования цели.
        enemyChase.AddTransition(EnemyStateKey.PlayerOffSight, enemyWait);

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

    public void SetTransition(EnemyStateKey transitionKey)
    {
        _stateMachine.TransitionNext(transitionKey);
    }

    private bool TargetInRange(Transform target, float range)
    {
        return Vector2.Distance(transform.position, target.position) <= range;
    }

    private void OnDisable()
    {
        MovePosition.StopMovement();
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