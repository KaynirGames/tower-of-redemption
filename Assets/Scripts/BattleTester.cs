﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleTester : MonoBehaviour
{
    public static event BattleManager.OnBattleTrigger OnBattleTrigger = delegate { return false; };

    [SerializeField] private Enemy _testEnemyPrefab = null;
    [SerializeField] private Transform _testSpawn = null;
    [SerializeField] private bool _isPlayerAdvantage = true;
    [SerializeField] private Skill _testSkill = null;

    private Player _player;
    private Enemy _currentEnemy;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (_currentEnemy == null)
            {
                _currentEnemy = Instantiate(_testEnemyPrefab, _testSpawn.position, Quaternion.identity);
                OnBattleTrigger.Invoke(_currentEnemy, _isPlayerAdvantage);
                if (_player == null) _player = GameMaster.Instance.ActivePlayer;
            }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            if (_testSkill.TargetType == TargetType.Enemy)
            {
                _testSkill.Activate(_player.PlayerStats, _currentEnemy.EnemyStats);
                Debug.Log($"Enemy {_currentEnemy.EnemySpec.name} took damage from {_testSkill.SkillName}");
                Debug.Log(_currentEnemy.EnemyStats.CurrentHealth);
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            Destroy(_currentEnemy.gameObject);
            _currentEnemy = null;
        }
    }
}
