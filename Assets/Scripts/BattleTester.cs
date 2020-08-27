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
    [SerializeField] private Skill _testSkill = null;

    private PlayerCharacter _player;
    private EnemyCharacter _currentEnemy;

    public SkillDescriptionUI descriptionUI;

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
                StartCoroutine(BattleStartRoutine());
                if (_player == null) _player = PlayerManager.Instance.Player;
            }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            if (_testSkill.TargetType == TargetType.Opponent)
            {
                _testSkill.Activate(_player, _currentEnemy);
                Debug.Log($"Enemy {_currentEnemy.EnemySpec.name} took damage from {_testSkill.SkillName}");
                Debug.Log(_currentEnemy.Stats.CurrentHealth);
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            Destroy(_currentEnemy.gameObject);
            _currentEnemy = null;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _player.Stats.MaxHealth.AddModifier(new StatModifier(10, ModifierType.Flat, this));
            _player.Stats.UpdateResourcesDisplay();
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            _player.Stats.MaxHealth.RemoveSourceModifiers(this);
            _player.Stats.UpdateResourcesDisplay();
        }
    }

    private IEnumerator BattleStartRoutine()
    {
        yield return new WaitForEndOfFrame();
        OnBattleTrigger.Invoke(_currentEnemy, _isPlayerAdvantage);
    }
}
