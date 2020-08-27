using System.Collections;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public delegate bool OnBattleTrigger(EnemyCharacter enemy, bool isPlayerAdvantage);

    public delegate void OnBattleEnd(bool isPlayerDeath);

    public event BattleDuration.OnBattleDurationExpire OnBattleDurationExpire = delegate { };

    [SerializeField] private BattleUI _battleUI = null;
    [Header("Бонусная энергия при боевом преимуществе:")]
    [SerializeField] private float _playerEnergyBonus = 0.25f;
    [SerializeField] private float _enemyEnergyBonus = 1f;

    private PlayerCharacter _player;
    private EnemyCharacter _enemy;

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
        EnemyCharacter.OnBattleEnd += EndBattle;

        BattleTester.OnBattleTrigger += StartBattle;
    }

    public Character FindTargetForSkillOwner(Character owner)
    {
        return owner == _player
            ? _enemy
            : _player as Character;
    }

    private bool StartBattle(EnemyCharacter enemy, bool isPlayerAdvantage)
    {
        GameMaster.Instance.TogglePause(true);

        if (_enemy == null)
        {
            _enemy = enemy;
            if (_player == null) { _player = PlayerManager.Instance.Player; }

            if (isPlayerAdvantage)
            {
                ApplyAdvantageEnergyBonus(_player.Stats, _playerEnergyBonus);
            }
            else
            {
                ApplyAdvantageEnergyBonus(enemy.Stats, _enemyEnergyBonus);
            }

            // Корутина перехода на экран боя с анимацией.

            _battleUI.ShowBattleWindow(_player, enemy);

            return true;
        }
        else { return false; }
    }

    private void EndBattle(bool isPlayerDeath)
    {
        GameMaster.Instance.TogglePause(false);

        if (isPlayerDeath)
        {
            // Сцена гибели.
            // Вывод меню с кнопками выхода.
        }
        else
        {
            _enemy = null;

            OnBattleDurationExpire?.Invoke(_player.Stats);

            StartCoroutine(CloseBattleRoutine());
        }
    }

    private void ApplyAdvantageEnergyBonus(CharacterStats stats, float bonusRate)
    {
        float energyBonus = Mathf.Round(stats.MaxEnergy.GetValue() * bonusRate);
        stats.RecoverEnergy(energyBonus);
    }

    private IEnumerator CloseBattleRoutine()
    {
        // Анимация выхода из боя.
        yield return null;
        _battleUI.CloseBattleWindow();
    }
}
