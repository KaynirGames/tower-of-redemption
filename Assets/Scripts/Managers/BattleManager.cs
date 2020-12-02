using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public delegate bool OnBattleTrigger(EnemyCharacter enemy, bool isPlayerAdvantage);
    public delegate void OnBattleEnd(bool isVictory);

    public static event Action OnBattleStart = delegate { };

    [SerializeField] private BattleUI _battleUI = null;
    [SerializeField] private SpiritGenerator _spiritGenerator = null;
    [Header("Настройки перехода в бой:")]
    [SerializeField] private AnimationController _transitionController = null;
    [SerializeField] private float _enemyActivationDelay = 3f;
    [Header("Бонусная энергия при боевом преимуществе:")]
    [SerializeField] private float _playerSpiritBonus = 0.25f;
    [SerializeField] private float _enemySpiritBonus = 1f;

    public SpiritGenerator SpiritGenerator => _spiritGenerator;

    private PlayerCharacter _player;
    private EnemyCharacter _enemy;
    private List<EnemyCharacter> _activeEnemies;

    private Vector3 _lastPlayerPosition;
    private Vector3 _lastEnemyPosition;

    private WaitForSeconds _waitBeforeEnemyActivation;

    private void Awake()
    {
        Instance = this;

        EnemyCharacter.OnBattleTrigger += StartBattle;
        PlayerAttackHit.OnBattleTrigger += StartBattle;

        EnemyCharacter.OnBattleEnd += EndBattle;
        PlayerCharacter.OnBattleEnd += EndBattle;
    }

    private void Start()
    {
        _waitBeforeEnemyActivation = new WaitForSeconds(_enemyActivationDelay);
        _activeEnemies = EnemyCharacter.ActiveEnemies;
    }

    private bool StartBattle(EnemyCharacter enemy, bool isPlayerAdvantage)
    {
        if (_enemy == null)
        {
            _enemy = enemy;
            _player = PlayerCharacter.Active;

            _activeEnemies.Remove(enemy);
            _activeEnemies.ForEach(en => en.gameObject.SetActive(false));

            _enemy.CurrentOpponent = _player;
            _player.CurrentOpponent = _enemy;

            HandleAdvantageBonus(isPlayerAdvantage);
            StartCoroutine(BattleStartRoutine());

            return true;
        }
        else { return false; }
    }

    private void PrepareBattlefield()
    {
        Room.ActiveRoom.ToggleBattleObstructingObjects(false);
        _battleUI.ShowBattleUI(_player, _enemy);
        _lastPlayerPosition = _player.transform.position;
        _lastEnemyPosition = _enemy.transform.position;

        _player.transform.position = _battleUI.PlayerPlacement.position;
        _enemy.transform.position = _battleUI.EnemyPlacement.position;
        _player.PrepareForBattle();
    }

    private void CloseBattlefield()
    {
        Room.ActiveRoom.ToggleBattleObstructingObjects(true);
        _battleUI.CloseBattleUI();
        _enemy.ExitBattle(_lastEnemyPosition);
        _player.ExitBattle(_lastPlayerPosition);
    }

    private void EndBattle(bool isVictory)
    {
        _player.CurrentOpponent = null;
        _enemy.CurrentOpponent = null;

        if (isVictory)
        {
            StartCoroutine(BattleVictoryRoutine());
        }
        else
        {
            StartCoroutine(BattleDefeatRoutine());
        }
    }

    private void HandleAdvantageBonus(bool isPlayerAdvantage)
    {
        CharacterStats stats = isPlayerAdvantage
            ? _player.Stats
            : _enemy.Stats;

        float spiritBonus = isPlayerAdvantage
            ? _playerSpiritBonus
            : _enemySpiritBonus;

        stats.ChangeSpirit(stats.Spirit.MaxValue.GetFinalValue()
                           * spiritBonus);
    }

    private IEnumerator BattleStartRoutine()
    {
        GameMaster.Instance.TogglePause(true);

        yield return _transitionController.PlayAndWaitForAnimRoutine("Enter",
                                                                     true,
                                                                     PrepareBattlefield);

        GameMaster.Instance.TogglePause(false);
        OnBattleStart.Invoke();

        yield return _waitBeforeEnemyActivation;

        _enemy.PrepareForBattle();
        _battleUI.ToggleBattleInteraction(true);
    }

    private IEnumerator BattleVictoryRoutine()
    {
        GameMaster.Instance.TogglePause(true);

        yield return _enemy.Animations.PlayAndWaitForAnimRoutine("Death",
                                                                 true);

        yield return _transitionController.PlayAndWaitForAnimRoutine("Victory",
                                                                     true,
                                                                     CloseBattlefield);

        GameMaster.Instance.TogglePause(false);

        yield return _waitBeforeEnemyActivation;

        _transitionController.SetParameter("Reset");
        _enemy = null;

        CheckIfRoomClear();
    }

    private IEnumerator BattleDefeatRoutine()
    {
        GameMaster.Instance.TogglePause(true);

        yield return _player.Animations.PlayAndWaitForAnimRoutine("Death",
                                                                  true);

        _transitionController.PlayAnimation("Defeat");

        GameMaster.Instance.TogglePause(false);
    }

    private void CheckIfRoomClear()
    {
        if (_activeEnemies.Count > 0)
        {
            _activeEnemies.ForEach(x => x.gameObject.SetActive(true));
        }
        else
        {
            Room.ActiveRoom.SetRoomStatus(true);
        }
    }
}