using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }
    /// <summary>
    /// Делегат для сообщения о начале боя.
    /// </summary>
    public delegate bool OnBattleTrigger(Enemy enemy, bool isPlayerAdvantage);
    /// <summary>
    /// Делегат для сообщения об окончании боя.
    /// </summary>
    public delegate void OnBattleEnd(bool isPlayerDeath);
    /// <summary>
    /// Событие для снятия эффектов с цели, действующих до конца боя.
    /// </summary>
    public event BattleDuration.OnBattleDurationExpire OnBattleDurationExpire = delegate { };
    
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
        Enemy.OnBattleEnd += EndBattle;

        BattleTester.OnBattleTrigger += StartBattle;
    }

    private bool StartBattle(Enemy enemy, bool isPlayerAdvantage)
    {
        //GameMaster.Instance.TogglePause();
        if (_enemy == null)
        {
            _enemy = enemy;
            if (_player == null) { _player = GameMaster.Instance.ActivePlayer; }

            Debug.Log(_player.PlayerSpec.name + " is now fighting " + _enemy.EnemySpec.name);

            // Задать начальные параметры.
            // Вызвать интерфейс для боя.
            return true;
        }
        else { return false; }
    }

    private void EndBattle(bool isPlayerDeath)
    {
        //GameMaster.Instance.TogglePause();
        Debug.Log("Battle is over.");
        OnBattleDurationExpire?.Invoke(_player.PlayerStats);
        // Действия для завершения боя.
    }
}
