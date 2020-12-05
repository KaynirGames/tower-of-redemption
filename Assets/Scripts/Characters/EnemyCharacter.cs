using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    public static event BattleManager.OnBattleTrigger OnBattleTrigger = delegate { return false; };
    public static event BattleManager.OnBattleEnd OnBattleEnd = delegate { };

    public static List<EnemyCharacter> ActiveEnemies = new List<EnemyCharacter>();

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
        SkillBook.LoadSkills(_enemySpec.BaseSkills);

        _enemyBattleAI.InitializeBattleAI(SkillBook);
        ActiveEnemies.Add(this);
    }

    public override void PrepareForBattle()
    {
        PlayMoveAnimation(Vector2.left);
        ToggleBattleAI(true);
    }

    public override void ExitBattle(Vector3 lastPosition)
    {
        base.ExitBattle(lastPosition);
        StopAllCoroutines();
        ToggleBattleAI(false);
        gameObject.SetActive(false);

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
        OnBattleEnd.Invoke(true);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.GetComponent<PlayerCharacter>() != null)
        {
            bool inBattle = OnBattleTrigger.Invoke(this, false);

            if (inBattle) { _enemyAI.enabled = false; }
        }
    }
}
