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
    /// <summary>
    /// Книга умений противника.
    /// </summary>
    public SkillBook SkillBook { get; private set; }

    private EnemyAI _enemyAI = null; // Основной ИИ противника.
    private EnemyBattleAI _enemyBattleAI = null; // Боевой ИИ противника.

    private void Awake()
    {
        EnemyStats = GetComponent<CharacterStats>();
        SkillBook = GetComponent<SkillBook>();
        _enemyAI = GetComponent<EnemyAI>();
        _enemyBattleAI = GetComponent<EnemyBattleAI>();

        EnemyStats.OnCharacterDeath += Die;
    }

    private void Start()
    {
        EnemyStats.SetBaseStats(_enemySpec);
        SkillBook.SetBaseSkills(_enemySpec);

        EnemyManager.Instance.RegisterEnemy(this);
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
