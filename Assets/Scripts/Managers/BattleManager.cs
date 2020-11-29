using System;
using System.Collections;
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
    [SerializeField] private float _transitionTime = 2f;
    [SerializeField] private float _deathAnimationTime = 2f;
    [SerializeField] private float _enemyActivationDelay = 3f;
    [Header("Бонусная энергия при боевом преимуществе:")]
    [SerializeField] private float _playerSpiritBonus = 0.25f;
    [SerializeField] private float _enemySpiritBonus = 1f;

    public SpiritGenerator SpiritGenerator => _spiritGenerator;

    private PlayerCharacter _player;
    private EnemyCharacter _enemy;

    private Vector3 _lastPlayerPosition;
    private Vector3 _lastEnemyPosition;

    private WaitForSecondsRealtime _waitForTransition;
    private WaitForSecondsRealtime _waitForDeathAnimation;
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
        _waitForTransition = new WaitForSecondsRealtime(_transitionTime);
        _waitForDeathAnimation = new WaitForSecondsRealtime(_deathAnimationTime);
        _waitBeforeEnemyActivation = new WaitForSeconds(_enemyActivationDelay);
    }

    private bool StartBattle(EnemyCharacter enemy, bool isPlayerAdvantage)
    {
        if (_enemy == null)
        {
            _enemy = enemy;
            _player = PlayerCharacter.Active;

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
        _battleUI.ShowBattleUI(_player, _enemy);
        _lastPlayerPosition = _player.transform.position;
        _lastEnemyPosition = _enemy.transform.position;

        _player.transform.position = _battleUI.PlayerPlacement.position;
        _enemy.transform.position = _battleUI.EnemyPlacement.position;
        _player.PrepareForBattle();
    }

    private void CloseBattlefield()
    {
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
        yield return _waitForDeathAnimation;

        GameMaster.Instance.TogglePause(true);
        //_transitionController.SetTrigger("Victory");

        yield return _waitForTransition;
        
        CloseBattlefield();
        GameMaster.Instance.TogglePause(false);

        yield return _waitBeforeEnemyActivation;

        //_transitionController.SetTrigger("Reset");
        _enemy = null;
    }

    private IEnumerator BattleDefeatRoutine()
    {
        yield return _waitForDeathAnimation;

        GameMaster.Instance.TogglePause(true);

        _transitionController.PlayAnimation("Defeat");
    }
}