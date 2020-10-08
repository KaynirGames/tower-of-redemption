using System.Collections;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public delegate bool OnBattleTrigger(EnemyCharacter enemy, bool isPlayerAdvantage);

    public delegate void OnBattleEnd(bool isPlayerDeath);

    [SerializeField] private BattleUI _battleUI = null;
    [SerializeField] private EnergyGenerator _energyGenerator = null;
    [SerializeField] private Transform _battlefieldPlacement = null;
    [SerializeField] private CameraController _cameraController = null;
    [Header("Бонусная энергия при боевом преимуществе:")]
    [SerializeField] private float _playerEnergyBonus = 0.25f;
    [SerializeField] private float _enemyEnergyBonus = 1f;

    public EnergyGenerator EnergyGenerator => _energyGenerator;

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
        PlayerAttackHit.OnBattleTrigger += StartBattle;

        EnemyCharacter.OnBattleEnd += EndBattle;

        BattleTester.OnBattleTrigger += StartBattle;
    }

    public Character FindBattleOpponent(Character character)
    {
        return character is PlayerCharacter 
            ? _enemy 
            : (Character)_player;
    }

    private bool StartBattle(EnemyCharacter enemy, bool isPlayerAdvantage)
    {
        //GameMaster.Instance.TogglePause(true);

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
                ApplyAdvantageEnergyBonus(_enemy.Stats, _enemyEnergyBonus);
            }

            _enemy.CurrentOpponent = _player;
            _player.CurrentOpponent = _enemy;

            // Корутина перехода на экран боя с анимацией.

            _battleUI.ShowBattleUI(_player, enemy);

            return true;
        }
        else { return false; }
    }

    private void SetBattlefield()
    {

    }

    private void EndBattle(bool isPlayerDeath)
    {
        //GameMaster.Instance.TogglePause(false);
        _player.CurrentOpponent = null;
        _enemy.CurrentOpponent = null;

        if (isPlayerDeath)
        {
            // Сцена гибели.
            // Вывод меню с кнопками выхода.
        }
        else
        {
            _enemy = null;
            StartCoroutine(CloseBattleRoutine());
        }
    }

    private void ApplyAdvantageEnergyBonus(CharacterStats stats, float bonusRate)
    {
        float energyBonus = stats.Energy.MaxValue.GetFinalValue()
                            * bonusRate;

        stats.ChangeEnergy(energyBonus);
    }

    private IEnumerator CloseBattleRoutine()
    {
        // Анимация выхода из боя.
        yield return null;
        _battleUI.CloseBattleUI();
    }
}
