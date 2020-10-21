using System;
using System.Collections;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public delegate bool OnBattleTrigger(EnemyCharacter enemy, bool isPlayerAdvantage);

    public delegate void OnBattleEnd(bool isPlayerDeath);

    [SerializeField] private BattleUI _battleUI = null;
    [SerializeField] private EnergyGenerator _energyGenerator = null;
    [SerializeField] private CameraController _cameraController = null;
    [Header("Бонусная энергия при боевом преимуществе:")]
    [SerializeField] private float _playerEnergyBonus = 0.25f;
    [SerializeField] private float _enemyEnergyBonus = 1f;

    public EnergyGenerator EnergyGenerator => _energyGenerator;

    private PlayerCharacter _player;
    private EnemyCharacter _enemy;

    private Vector3 _lastPlayerPosition;
    private Vector3 _lastEnemyPosition;

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

        EnemyCharacter.OnBattleTrigger += StartBattle;
        PlayerAttackHit.OnBattleTrigger += StartBattle;

        EnemyCharacter.OnBattleEnd += EndBattle;

        BattleTester.OnBattleTrigger += StartBattle;
    }

    private bool StartBattle(EnemyCharacter enemy, bool isPlayerAdvantage)
    {
        if (_enemy == null)
        {
            _enemy = enemy;
            _player = PlayerManager.Instance.Player;

            _enemy.CurrentOpponent = _player;
            _player.CurrentOpponent = _enemy;

            HandleAdvantageBonus(isPlayerAdvantage);

            PrepareBattlefield();

            _battleUI.ShowBattleUI(_player, _enemy);

            _enemy.PrepareForBattle();

            return true;
        }
        else { return false; }
    }

    private void PrepareBattlefield()
    {
        _lastPlayerPosition = _player.transform.position;
        _lastEnemyPosition = _enemy.transform.position;

        _player.transform.position = GetBattlePosition(_battleUI.PlayerPlacement);
        _enemy.transform.position = GetBattlePosition(_battleUI.EnemyPlacement);

        _player.PrepareForBattle();
    }

    private void EndBattle(bool isPlayerDeath)
    {
        _player.CurrentOpponent = null;
        _enemy.CurrentOpponent = null;

        if (isPlayerDeath)
        {
            // Переход на Game Over сцену.
        }
        else
        {
            StartCoroutine(CloseBattleRoutine());
        }
    }

    private void HandleAdvantageBonus(bool isPlayerAdvantage)
    {
        CharacterStats stats = isPlayerAdvantage
            ? _player.Stats
            : _enemy.Stats;

        float energyBonus = isPlayerAdvantage
            ? _playerEnergyBonus
            : _enemyEnergyBonus;

        stats.ChangeEnergy(stats.Energy.MaxValue.GetFinalValue()
                           * energyBonus);
    }

    private Vector3 GetBattlePosition(Transform battleUIPlacement)
    {
        Vector3 position = _cameraController.CurrentCamera
                                            .ScreenToWorldPoint(battleUIPlacement.position);
        position.z = 0;

        return position;
    }

    private IEnumerator CloseBattleRoutine()
    {
        // Анимация выхода из боя.
        _battleUI.CloseBattleUI();
        _enemy = null;
        yield return null;
        _player.transform.position = _lastPlayerPosition;
    }
}
