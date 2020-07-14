﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public static BattleSystem Instance { get; private set; }

    public bool IsBattleActive { get; private set; }
    
    private Enemy _currentEnemy;
    private Player _player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Enemy.OnBattleTrigger += StartBattle;
    }

    private void StartBattle(Enemy enemy, bool isPlayerAdvantage)
    {
        IsBattleActive = true;
        _currentEnemy = enemy;
        _player = GameMaster.Instance.Player;
        // Задать начальные параметры.
        // Вызвать интерфейс для боя.
    }

    private void EndBattle()
    {
        IsBattleActive = false;
        // Действия для завершения боя.
    }
}
