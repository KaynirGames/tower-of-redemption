using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action<Enemy> OnEnemyDeath = delegate { };

    [SerializeField] private EnemySpec spec = null; // Спек противника.

    private bool FacingRight = true; // Направление взгляда противника.
    private CharacterStats enemyStats; // Статы противника.

    private void Awake()
    {
        enemyStats = GetComponent<CharacterStats>();
    }

    private void Start()
    {
        enemyStats.SetStats(spec);
        enemyStats.OnCharacterDeath += Die;
        EnemyManager.Instance.RegisterEnemy(this);
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
        OnEnemyDeath.Invoke(this);
        // Выйти из боевой системы.
        // Заспавнить лут.
        // Уничтожить объект.
    }
}
