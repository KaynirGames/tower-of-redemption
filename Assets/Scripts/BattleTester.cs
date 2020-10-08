using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class BattleTester : MonoBehaviour
{
    public static event BattleManager.OnBattleTrigger OnBattleTrigger = delegate { return false; };

    [SerializeField] private EnemyCharacter _testEnemyPrefab = null;
    [SerializeField] private Transform _testSpawn = null;
    [SerializeField] private bool _isPlayerAdvantage = true;
    [SerializeField] Joystick _joystick = null;

    private PlayerCharacter _player;
    private EnemyCharacter _currentEnemy;

    public DescriptionUI descriptionUI;

    private void Start()
    {
        //descriptionUI.ShowDescription(_testSkill);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (_currentEnemy == null)
            {
                _currentEnemy = Instantiate(_testEnemyPrefab, _testSpawn.position, Quaternion.identity);
                //StartCoroutine(BattleStartRoutine());
                if (_player == null) _player = PlayerManager.Instance.Player;
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            _joystick.enabled = !_joystick.enabled;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            _player.Stats.ChangeHealth(-20);
        }
    }

    private IEnumerator BattleStartRoutine()
    {
        yield return new WaitForEndOfFrame();
        OnBattleTrigger.Invoke(_currentEnemy, _isPlayerAdvantage);
    }
}
