using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static BattleManager.OnBattleTrigger OnBattleTrigger = delegate { };

    [SerializeField] private EnemySpec _enemySpec = null; // Спек противника.

    private bool FacingRight = true; // Направление взгляда противника.
    private CharacterStats _enemyStats; // Статы противника.
    private EnemyAI _enemyAI = null; // Основной ИИ противника.
    private EnemyBattleAI _enemyBattleAI = null; // Боевой ИИ противника.

    private void Awake()
    {
        _enemyStats = GetComponent<CharacterStats>();
        _enemyAI = GetComponent<EnemyAI>();
        _enemyBattleAI = GetComponent<EnemyBattleAI>();
    }

    private void Start()
    {
        _enemyStats.SetBaseStats(_enemySpec);
        _enemyStats.OnCharacterDeath += Die;
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
        // Выйти из боевой системы.
        // Заспавнить лут.
        // Уничтожить объект.
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            OnBattleTrigger?.Invoke(this, false);
            _enemyAI.enabled = false;
            _enemyBattleAI.enabled = true;
        }
    }
}
