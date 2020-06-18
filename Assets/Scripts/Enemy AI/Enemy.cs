using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemySpec enemySpec = null; // Спек противника.
    [SerializeField] protected Transform attackPoint = null; // Точка, откуда производится атака.

    [SerializeField] private float moveSpeed = 2f; // Скорость перемещения.
    [SerializeField] private float viewDistance = 4f; // Дальность обзора.
    [SerializeField] private float attackDistance = 2f; // Расстояние, на котором выполняется атака цели.
    [SerializeField] private float attackCooldown = 1f; // Время перезарядки до следующей атаки.
    [SerializeField] private PlayerRuntimeSet activePlayerRS = null; // Набор с активным игроком.

    public float MoveSpeed => moveSpeed;
    public float ViewDistance => viewDistance;
    public float AttackDistance => attackDistance;
    public float AttackCooldown => attackCooldown;

    public Transform Target { get; private set; }
    public Animator Animator { get; private set; }
    public bool AttackIsComplete { get; set; }

    private StateMachine stateMachine;

    private CharacterStats enemyStats;

    private void Awake()
    {
        enemyStats = GetComponent<CharacterStats>();
        Animator = GetComponent<Animator>();
        stateMachine = GetComponent<StateMachine>();
    }

    private void Start()
    {
        Target = activePlayerRS.GetObject(0).transform;

        Dictionary<Type, BaseState> availableStates = new Dictionary<Type, BaseState>()
        {
            { typeof(PatrolState), new PatrolState(this) },
            { typeof(FollowState), new FollowState(this) },
            { typeof(AttackState), new AttackState(this) }
        };

        stateMachine.SetStates(availableStates);

        enemyStats.SetCharacterStats(enemySpec);
        enemyStats.OnCharacterDeath += Die;
    }
    /// <summary>
    /// Проверить, находится ли цель на заданном расстоянии.
    /// </summary>
    /// <param name="range">Расстояние.</param>
    public bool IsTargetInRange(float range)
    {
        return Vector2.Distance(transform.position, Target.position) <= range;
    }

    private void Die()
    {
        // Выйти из боевой системы.
        // Заспавнить лут.
        // Уничтожить объект.
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewDistance);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
    /// <summary>
    /// Атака ближнего боя на определенном фрейме анимации.
    /// </summary>
    public void MeleeAttack()
    {
        AttackIsComplete = true;
    }
    /// <summary>
    /// Атака дальнего боя на определенном фрейме анимации.
    /// </summary>
    public void RangeAttack()
    {

    }
}
