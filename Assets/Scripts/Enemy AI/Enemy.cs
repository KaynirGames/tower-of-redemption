using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaynirGames.Pathfinding;
using KaynirGames.Movement;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemySpec spec = null; // Спек противника.
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
    /// Объект, нуждающийся в оптимальном маршруте.
    /// </summary>
    public Seeker Seeker { get; private set; }

    private bool FacingRight = true; // Направление взгляда противника.
    private CharacterStats enemyStats;

    private void Awake()
    {
        enemyStats = GetComponent<CharacterStats>();
        Animator = GetComponent<Animator>();
        Seeker = GetComponent<Seeker>();
    }

    private void Start()
    {
        //Seeker.SetFollowSpeed(spec.MoveSpeed);

        enemyStats.SetStats(spec);
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spec.ViewDistance);
    }
}
