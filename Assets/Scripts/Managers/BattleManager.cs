using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public delegate void OnBattleStatusChange(bool isActive);
    public delegate void OnBattleTrigger(Enemy enemyBattleWith, bool isPlayerAdvantage);

    public OnBattleStatusChange OnStatusChange = delegate { };

    //public bool IsBattleActive { get; private set; }
    
    private Player _player;
    private Enemy _enemy;

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
        //IsBattleActive = true;
        GameMaster.Instance.TogglePause();
        OnStatusChange?.Invoke(true);
        _enemy = enemy;
        _player = GameMaster.Instance.ActivePlayer;
        // Задать начальные параметры.
        // Вызвать интерфейс для боя.
    }

    private void EndBattle()
    {
        GameMaster.Instance.TogglePause();
        //IsBattleActive = false;
        OnStatusChange?.Invoke(false);
        // Действия для завершения боя.
    }
}
