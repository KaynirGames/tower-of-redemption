using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event BattleManager.OnBattleTrigger OnBattleTrigger = delegate { return false; };
    public static event BattleManager.OnBattleEnd OnBattleEnd = delegate { };

    [SerializeField] private EnemySpec _enemySpec = null;
    /// <summary>
    /// Специализация противника.
    /// </summary>
    public EnemySpec EnemySpec => _enemySpec;
    /// <summary>
    /// Статы противника.
    /// </summary>
    public CharacterStats EnemyStats { get; private set; }

    private bool FacingRight = true; // Направление взгляда противника.
    private EnemyAI _enemyAI = null; // Основной ИИ противника.
    private EnemyBattleAI _enemyBattleAI = null; // Боевой ИИ противника.

    private void Awake()
    {
        EnemyStats = GetComponent<CharacterStats>();
        _enemyAI = GetComponent<EnemyAI>();
        _enemyBattleAI = GetComponent<EnemyBattleAI>();
    }

    private void Start()
    {
        EnemyStats.SetBaseStats(_enemySpec);
        EnemyStats.OnCharacterDeath += Die;
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
        OnBattleEnd.Invoke(false);
        Destroy(gameObject);
        // Выйти из боевой системы.
        // Заспавнить лут.
        // Уничтожить объект.
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            bool inBattle = OnBattleTrigger.Invoke(this, false);

            if (inBattle)
            {
                _enemyAI.enabled = false;
                _enemyBattleAI.enabled = true;
            }
        }
    }
}
