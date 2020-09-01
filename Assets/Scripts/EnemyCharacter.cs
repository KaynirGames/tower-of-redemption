using System;
using UnityEngine;

public class EnemyCharacter : Character
{
    public static event BattleManager.OnBattleTrigger OnBattleTrigger = delegate { return false; };
    public static event BattleManager.OnBattleEnd OnBattleEnd = delegate { };

    [SerializeField] private EnemySpec _enemySpec = null;
    /// <summary>
    /// Специализация противника.
    /// </summary>
    public EnemySpec EnemySpec => _enemySpec;

    private EnemyAI _enemyAI = null; // Основной ИИ противника.
    private EnemyBattleAI _enemyBattleAI = null; // Боевой ИИ противника.

    private void Awake()
    {
        Stats = GetComponent<CharacterStats>();
        SkillBook = GetComponent<SkillBook>();

        _enemyAI = GetComponent<EnemyAI>();
        _enemyBattleAI = GetComponent<EnemyBattleAI>();

        Stats.OnCharacterDeath += Die;
    }

    private void Start()
    {
        Stats.SetCharacterStats(_enemySpec);
        SkillBook.SetBaseSpecSkills(_enemySpec);

        EnemyManager.Instance.RegisterEnemy(this);
    }

    protected override void Die()
    {
        OnBattleEnd.Invoke(false);
        Destroy(gameObject);
        // Выйти из боевой системы.
        // Заспавнить лут.
        // Уничтожить объект.
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerCharacter>() != null)
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
