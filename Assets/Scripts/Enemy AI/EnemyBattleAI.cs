using KaynirGames.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleAI : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _healthRateForDefence = 0.6f;
    [SerializeField, Range(0, 1)] private float _healthRateForEnrage = 0.2f;
    [SerializeField] private int _enrageAmount = 0;

    public CharacterResource Health { get; private set; }
    public CharacterResource Energy { get; private set; }

    private StateMachine<EnemyBattleStateKey> _stateMachine;

    private List<Skill> _attackSkills;
    private List<Skill> _defenceSkills;
    private List<Skill> _specialSkills;

    private EnemyCharacter _enemy;
    private float _energyRegen;
    private float _energyRegenDelay;

    private Coroutine _lastEnergyRegenRoutine;
    private WaitForSeconds _waitForEnergyRegenDelay;

    private void Awake()
    {
        _attackSkills = new List<Skill>();
        _defenceSkills = new List<Skill>();
        _specialSkills = new List<Skill>();

        _enemy = GetComponent<EnemyCharacter>();
    }

    private void Update()
    {
        _stateMachine.Update();

        if (Health.CurrentValue >= _healthRateForDefence * Health.MaxValue.GetFinalValue())
        {
            _stateMachine.TransitionNext(EnemyBattleStateKey.TryAttack);
        }
        else
        {
            if (Health.CurrentValue >= _healthRateForEnrage * Health.MaxValue.GetFinalValue())
            {
                _stateMachine.TransitionNext(EnemyBattleStateKey.TryDefence);
            }
            else
            {
                if (_enrageAmount > 0)
                {
                    _enrageAmount--;
                    _stateMachine.TransitionNext(EnemyBattleStateKey.TryEnrage);
                }
                else
                {
                    _stateMachine.TransitionNext(EnemyBattleStateKey.TryDefence);
                }
            }
        }
    }

    public void InitializeBattleAI(SkillBook skillBook)
    {
        Health = _enemy.Stats.Health;
        Energy = _enemy.Stats.Spirit;

        _energyRegen = _enemy.EnemySpec.EnergyRegen;
        _energyRegenDelay = _enemy.EnemySpec.EnergyRegenDelay;
        _waitForEnergyRegenDelay = new WaitForSeconds(_energyRegenDelay);

        CollectSkills(skillBook);
        CreateAvailableStates();
    }

    public void ToggleEnergyRegen(bool enable)
    {
        if (enable && gameObject.activeSelf)
        {
            _lastEnergyRegenRoutine = StartCoroutine(EnergyRegenRoutine());
        }
        else
        {
            if (_lastEnergyRegenRoutine != null)
            {
                StopCoroutine(_lastEnergyRegenRoutine);
            }
        }
    }

    public void SetTransition(EnemyBattleStateKey battleStateKey)
    {
        _stateMachine.TransitionNext(battleStateKey);
    }

    public Skill GetSuitableSkill(Dictionary<Skill, int> skillWeights)
    {
        Skill skillInstance = null;
        int minWeight = int.MaxValue;

        foreach (var pair in skillWeights)
        {
            if (pair.Key.IsCooldown) continue;

            if (pair.Value <= minWeight)
            {
                minWeight = pair.Value;
                skillInstance = pair.Key;
            }
        }

        return skillInstance;
    }

    private void CollectSkills(SkillBook skillBook)
    {
        var activeSkills = skillBook.GetSkillSlots(SkillSlot.Active);

        foreach (Skill skill in activeSkills)
        {
            if (skill == null) { continue; }

            if (skill.SkillSO.GetType() == typeof(AttackSkillSO))
            {
                _attackSkills.Add(skill);
            }
            else if (skill.SkillSO.GetType() == typeof(DefenceSkillSO))
            {
                _defenceSkills.Add(skill);
            }
        }

        var specialSkills = skillBook.GetSkillSlots(SkillSlot.Special);

        foreach (Skill skill in specialSkills)
        {
            if (skill == null) { continue; }
            _specialSkills.Add(skill);
        }
    }

    private void CreateAvailableStates()
    {
        EnemyAttack enemyAttack = new EnemyAttack(this, CreateSkillWeights(_attackSkills));
        EnemyDefence enemyDefence = null;

        if (_defenceSkills.Count > 0)
        {
            enemyDefence = new EnemyDefence(this, CreateSkillWeights(_defenceSkills));

            enemyAttack.AddTransition(EnemyBattleStateKey.TryDefence, enemyDefence);
            enemyDefence.AddTransition(EnemyBattleStateKey.TryAttack, enemyAttack);
        }

        if (_specialSkills.Count > 0 && _enrageAmount > 0)
        {
            EnemyEnrage enemyEnrage = new EnemyEnrage(this, CreateSkillWeights(_specialSkills));

            enemyAttack.AddTransition(EnemyBattleStateKey.TryEnrage, enemyEnrage);

            if (enemyDefence != null)
            {
                enemyDefence.AddTransition(EnemyBattleStateKey.TryEnrage, enemyEnrage);
            }
        }

        _stateMachine = new StateMachine<EnemyBattleStateKey>(enemyAttack);
    }

    private IEnumerator EnergyRegenRoutine()
    {
        while (true)
        {
            yield return _waitForEnergyRegenDelay;

            Energy.ChangeResource(_energyRegen);
        }
    }

    private Dictionary<Skill, int> CreateSkillWeights(List<Skill> skills)
    {
        Dictionary<Skill, int> skillWeights = new Dictionary<Skill, int>();

        foreach (Skill instance in skills)
        {
            int energyPerSecond = Mathf.RoundToInt(_energyRegen / _energyRegenDelay);
            int weight = instance.SkillSO.Cooldown * energyPerSecond - instance.SkillSO.Cost;

            skillWeights.Add(instance, weight);
        }

        return skillWeights;
    }
}
