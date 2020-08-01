using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public static BattleSystem Instance { get; private set; }

    public event Action OnBattleEnd = delegate { };

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
        _player = GameMaster.Instance.ActivePlayer;
        // Задать начальные параметры.
        // Вызвать интерфейс для боя.
    }

    private void EndBattle()
    {
        IsBattleActive = false;
        OnBattleEnd?.Invoke();
        // Действия для завершения боя.
    }
}
