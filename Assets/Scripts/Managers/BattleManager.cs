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

            // Корутина перехода на экран боя с анимацией.

            TogglePassiveSkillsForOpponent(_player, _enemy, true);
            TogglePassiveSkillsForOpponent(_enemy, _player, true);

            _battleUI.ShowBattleWindow(_player, enemy);

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

        if (isPlayerDeath)
        {
            // Сцена гибели.
            // Вывод меню с кнопками выхода.
        }
        else
        {
            TogglePassiveSkillsForOpponent(_player, _enemy, false);
            TogglePassiveSkillsForOpponent(_enemy, _player, false);

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

    private void TogglePassiveSkillsForOpponent(Character owner, Character opponent, bool enable)
    {
        //SkillSlot[] passiveSlots = owner.SkillBook.GetBookSlots(SkillSlotType.PassiveSlot);

        //foreach (SkillSlot slot in passiveSlots)
        //{
        //    if (!slot.IsEmpty)
        //    {
        //        if (enable)
        //        {
        //            slot.Skill.Activate(null, opponent);
        //        }
        //        else
        //        {
        //            slot.Skill.Deactivate(null, opponent);
        //        }
        //    }
        //}
    }

    private IEnumerator CloseBattleRoutine()
    {
        // Анимация выхода из боя.
        yield return null;
        _battleUI.CloseBattleWindow();
    }
}
