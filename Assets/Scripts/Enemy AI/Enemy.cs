using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemySpec spec = null; // Спек противника.
    [SerializeField] private Transform attackPoint = null; // Точка, откуда производится атака.
    [SerializeField] private PlayerRuntimeSet activePlayerRS = null; // Набор с активным игроком.

    /// <summary>
    /// Специализация противника.
    /// </summary>
    public EnemySpec Spec => spec;
    /// <summary>
    /// Цель противника.
    /// </summary>
    public Transform Target { get; private set; }
    /// <summary>
    /// Аниматор противника.
    /// </summary>
    public Animator Animator { get; private set; }
    /// <summary>
    /// Доступно ли перемещение в настоящий момент?
    /// </summary>
    public bool CanMove { get; private set; }
    /// <summary>
    /// Доступна ли атака в настоящий момент?
    /// </summary>
    private bool CanAttack;
    /// <summary>
    /// Направление взгляда противника.
    /// </summary>
    private bool FacingRight = true;

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
        CanAttack = true;

        SetupStateMachine();

        enemyStats.SetStats(spec);
        enemyStats.OnCharacterDeath += Die;
    }
    /// <summary>
    /// Настроить конечный автомат для обработки поведения врага.
    /// </summary>
    private void SetupStateMachine()
    {
        Dictionary<Type, BaseState> availableStates = new Dictionary<Type, BaseState>()
        {
            { typeof(EnemyPatrol), new EnemyPatrol(this) },
            { typeof(EnemyFollow), new EnemyFollow(this) },
            { typeof(EnemyAttack), new EnemyAttack(this) }
        };

        stateMachine.SetStates(availableStates);
    }
    /// <summary>
    /// Проверить, находится ли цель на заданном расстоянии.
    /// </summary>
    /// <param name="range">Расстояние.</param>
    public bool IsTargetInRange(float range)
    {
        return Vector2.Distance(transform.position, Target.position) <= range;
    }
    /// <summary>
    /// Провести атаку, если она доступна.
    /// </summary>
    public void TryAttack()
    {
        if (CanAttack)
        {
            CanMove = false;
            Animator.SetTrigger("Attack");
            StartCoroutine(SetAttackCooldown());
        }
    }
    /// <summary>
    /// Активировать перезарядку атаки.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SetAttackCooldown()
    {
        CanAttack = false;
        yield return new WaitForSeconds(spec.AttackCooldown);
        CanAttack = true;
    }
    /// <summary>
    /// Атака ближнего боя на определенном фрейме анимации.
    /// </summary>
    public void MeleeAttack()
    {
        // Атака ближнего боя.
        CanMove = true;
    }
    /// <summary>
    /// Атака дальнего боя на определенном фрейме анимации.
    /// </summary>
    public void RangeAttack()
    {
        // Атака дальнего боя.
        CanMove = true;
    }

    /// <summary>
    /// Повернуть спрайт противника в сторону цели.
    /// </summary>
    /// <param name="targetPos">Позиция цели.</param>
    public void FaceTarget(Vector2 targetPos)
    {
        float relativePosX = targetPos.x - transform.position.x;

        if (relativePosX < 0 && FacingRight || relativePosX > 0 && !FacingRight)
        {
            FacingRight = !FacingRight;

            Vector3 flipLocalScale = transform.localScale;
            flipLocalScale.x *= -1;
            transform.localScale = flipLocalScale;
        }
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
        Gizmos.DrawWireSphere(transform.position, spec.ViewDistance);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackPoint.transform.position, spec.AttackDistance);
    }
}
