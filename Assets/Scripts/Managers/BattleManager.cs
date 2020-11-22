using System.Collections;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public delegate bool OnBattleTrigger(EnemyCharacter enemy, bool isPlayerAdvantage);

    public delegate void OnBattleEnd(bool isVictory);

    [SerializeField] private BattleUI _battleUI = null;
    [SerializeField] private SpiritGenerator _spiritGenerator = null;
    [Header("Настройки перехода в бой:")]
    [SerializeField] private Animator _battleEnterTransition = null;
    [SerializeField] private Animator _battleOutTransition = null;
    [SerializeField] private float _timeForBattleTransition = 1f;
    [SerializeField] private float _timeForDeathAnimation = 1f;
    [SerializeField] private float _timeBeforeEnemyActivation = 2f;
    [Header("Бонусная энергия при боевом преимуществе:")]
    [SerializeField] private float _playerSpiritBonus = 0.25f;
    [SerializeField] private float _enemySpiritBonus = 1f;

    public SpiritGenerator SpiritGenerator => _spiritGenerator;

    private PlayerCharacter _player;
    private EnemyCharacter _enemy;

    private Vector3 _lastPlayerPosition;
    private Vector3 _lastEnemyPosition;

    private WaitForSecondsRealtime _waitForBattleTransition;
    private WaitForSecondsRealtime _waitForDeathAnimation;
    private WaitForSecondsRealtime _waitBeforeEnemyActivation;

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
        PlayerCharacter.OnBattleEnd += EndBattle;
    }

    private void Start()
    {
        _waitForBattleTransition = new WaitForSecondsRealtime(_timeForBattleTransition);
        _waitForDeathAnimation = new WaitForSecondsRealtime(_timeForDeathAnimation);
        _waitBeforeEnemyActivation = new WaitForSecondsRealtime(_timeBeforeEnemyActivation);
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
        _lastPlayerPosition = _player.transform.position;
        _lastEnemyPosition = _enemy.transform.position;

        _player.transform.position = _battleUI.PlayerPlacement.position;
        _enemy.transform.position = _battleUI.EnemyPlacement.position;
        _player.PrepareForBattle();
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

        _battleEnterTransition.Rebind();
        _battleEnterTransition.enabled = true;

        yield return _waitForBattleTransition;

        PrepareBattlefield();
        _battleUI.ShowBattleUI(_player, _enemy);

        _battleEnterTransition.SetTrigger("End");

        GameMaster.Instance.TogglePause(false);

        yield return _waitBeforeEnemyActivation;

        _enemy.PrepareForBattle();
        _battleEnterTransition.enabled = false;
    }

    private IEnumerator BattleVictoryRoutine()
    {
        GameMaster.Instance.TogglePause(true);

        yield return _waitForDeathAnimation;

        _battleOutTransition.Rebind();
        _battleOutTransition.enabled = true;

        yield return _waitForBattleTransition;

        _enemy.ExitBattle(_lastEnemyPosition);
        _player.ExitBattle(_lastPlayerPosition);
        _battleUI.CloseBattleUI();

        _battleOutTransition.SetTrigger("Victory");

        GameMaster.Instance.TogglePause(false);

        yield return _waitBeforeEnemyActivation;

        _battleOutTransition.enabled = false;
        _enemy = null;
    }

    private IEnumerator BattleDefeatRoutine()
    {
        GameMaster.Instance.TogglePause(true);

        yield return _waitForDeathAnimation;

        _battleOutTransition.Rebind();
        _battleOutTransition.enabled = true;

        yield return _waitForBattleTransition;
        
        _battleOutTransition.SetTrigger("Defeat");
    }
}