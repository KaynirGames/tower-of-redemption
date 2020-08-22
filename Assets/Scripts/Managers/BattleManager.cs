using System;
using System.Collections;
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

    [SerializeField] private BattleUI _battleUI = null;
    [Header("Бонусная энергия при боевом преимуществе:")]
    [SerializeField] private float _playerEnergyBonus = 0.25f;
    [SerializeField] private float _enemyEnergyBonus = 1f;

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

        SkillSlotUI.OnPlayerActivationCall += ActivateSkill;

        BattleTester.OnBattleTrigger += StartBattle;
    }
    /// <summary>
    /// Начать бой.
    /// </summary>
    private bool StartBattle(Enemy enemy, bool isPlayerAdvantage)
    {
        GameMaster.Instance.TogglePause(true);

        if (_enemy == null)
        {
            _enemy = enemy;
            if (_player == null) { _player = PlayerManager.Instance.Player; }

            if (isPlayerAdvantage)
            {
                ApplyEnergyBonus(_player.PlayerStats, _playerEnergyBonus); 
            }
            else
            {
                ApplyEnergyBonus(enemy.EnemyStats, _enemyEnergyBonus);
            }

            // Корутина перехода на экран боя с анимацией.

            _battleUI.ShowBattleWindow(_player, enemy);

            return true;
        }
        else { return false; }
    }
    /// <summary>
    /// Завершить бой.
    /// </summary>
    private void EndBattle(bool isPlayerDeath)
    {
        GameMaster.Instance.TogglePause(false);

        OnBattleDurationExpire?.Invoke(_player.PlayerStats);
        // Корутина закрытия экрана боя с анимацией.
        _battleUI.CloseBattleWindow();
    }
    /// <summary>
    /// Применить бонус к энергии персонажа.
    /// </summary>
    private void ApplyEnergyBonus(CharacterStats stats, float bonusRate)
    {
        float energyBonus = Mathf.Round(stats.MaxEnergy.GetValue() * bonusRate);
        stats.RecoverEnergy(energyBonus);
    }
    /// <summary>
    /// Активировать умение.
    /// </summary>
    private void ActivateSkill(Skill skill)
    {
        switch (skill.TargetType)
        {
            case TargetType.Enemy:
                skill.Activate(_player.PlayerStats, _enemy.EnemyStats);
                break;
            case TargetType.Self:
                skill.Activate(_player.PlayerStats, _player.PlayerStats);
                break;
        }
    }
}
