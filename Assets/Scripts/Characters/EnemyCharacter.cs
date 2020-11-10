using UnityEngine;

public class EnemyCharacter : Character
{
    public static event BattleManager.OnBattleTrigger OnBattleTrigger = delegate { return false; };
    public static event BattleManager.OnBattleEnd OnBattleEnd = delegate { };

    [SerializeField] private EnemySpec _enemySpec = null;

    public EnemySpec EnemySpec => _enemySpec;

    private EnemyAI _enemyAI = null;
    private EnemyBattleAI _enemyBattleAI = null;

    protected override void Awake()
    {
        base.Awake();

        _enemyAI = GetComponent<EnemyAI>();
        _enemyBattleAI = GetComponent<EnemyBattleAI>();

        Stats.OnCharacterDeath += Die;
    }

    private void Start()
    {
        Stats.SetCharacterStats(_enemySpec);
        SkillBook.SetBaseSpecSkills(_enemySpec);

        _enemyBattleAI.InitializeBattleAI(SkillBook);

        EnemyManager.Instance.RegisterEnemy(this);
    }

    public override void PrepareForBattle()
    {
        ToggleBattleAI(true);
    }

    public override void ExitBattle(Vector3 lastPosition)
    {
        gameObject.SetActive(false);
        base.ExitBattle(lastPosition);

        // Заспавнить лут.
        // Уничтожить объект.
    }

    private void ToggleBattleAI(bool enable)
    {
        _enemyBattleAI.enabled = enable;
        _enemyBattleAI.ToggleEnergyRegen(enable);
    }

    protected override void Die()
    {
        // Death Animation (unscaled time)
        OnBattleEnd.Invoke(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerCharacter>() != null)
        {
            bool inBattle = OnBattleTrigger.Invoke(this, false);

            if (inBattle) { _enemyAI.enabled = false; }
        }
    }
}
