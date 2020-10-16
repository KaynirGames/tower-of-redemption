﻿using System;
using UnityEngine;

public class EnemyCharacter : Character
{
    public static event BattleManager.OnBattleTrigger OnBattleTrigger = delegate { return false; };
    public static event BattleManager.OnBattleEnd OnBattleEnd = delegate { };

    [SerializeField] private EnemySpec _enemySpec = null;

    public EnemySpec EnemySpec => _enemySpec;

    private EnemyAI _enemyAI = null; // Основной ИИ противника.
    private EnemyBattleAI _enemyBattleAI = null; // Боевой ИИ противника.

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

        EnemyManager.Instance.RegisterEnemy(this);
    }

    public override void PrepareForBattle()
    {
        _enemyAI.enabled = false;
        _enemyBattleAI.enabled = true;
    }

    protected override void Die()
    {
        SkillBook.TogglePassiveBattleEffects(false);
        Effects.DisableAllEffects();

        gameObject.SetActive(false);

        OnBattleEnd.Invoke(false);

        //Destroy(gameObject);

        // Выйти из боевой системы.
        // Заспавнить лут.
        // Уничтожить объект.
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerCharacter>() != null)
        {
            bool inBattle = OnBattleTrigger.Invoke(this, false);

            if (inBattle) { PrepareForBattle(); }
        }
    }
}