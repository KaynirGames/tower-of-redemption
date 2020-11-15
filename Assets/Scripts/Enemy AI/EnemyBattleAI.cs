using KaynirGames.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleAI : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _healthRateForDefence = 0.6f;
    [SerializeField, Range(0, 1)] private float _healthRateForEnrage = 0.2f;

    public CharacterResource Health { get; private set; }
    public CharacterResource Energy { get; private set; }

    private StateMachine<EnemyBattleStateKey> _stateMachine;

    private List<Skill> _attackSkills;
    private List<Skill> _defenceSkills;
    private Skill _specialSkill;

    private EnemyCharacter _enemy;
    private float _energyRegen;
    private float _energyRegenDelay;

    private Coroutine _lastEnergyRegenRoutine;
    private WaitForSeconds _waitForEnergyRegenDelay;

    private void Awake()
    {
        _attackSkills = new List<Skill>();
        _defenceSkills = new List<Skill>();

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
                _stateMachine.TransitionNext(EnemyBattleStateKey.TryEnrage);
            }
        }
    }

    public void InitializeBattleAI(SkillBook skillBook)
    {
        Health = _enemy.Stats.Health;
        Energy = _enemy.Stats.Energy;

        _energyRegen = _enemy.EnemySpec.EnergyRegen;
        _energyRegenDelay = _enemy.EnemySpec.EnergyRegenDelay;
        _waitForEnergyRegenDelay = new WaitForSeconds(_energyRegenDelay);

        CollectSkills(skillBook);
        CreateAvailableStates();
    }

    public void ToggleEnergyRegen(bool enable)
    {
        if (enable)
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

        foreach (Skill instance in activeSkills)
        {
            if (instance == null) { continue; }

            if (instance.SkillSO.GetType() == typeof(AttackSkillSO))
            {
                _attackSkills.Add(instance);
            }
            else if (instance.SkillSO.GetType() == typeof(DefenceSkillSO))
            {
                _defenceSkills.Add(instance);
            }
        }

        _specialSkill = skillBook.GetSkillSlots(SkillSlot.Special)[0];
    }

    private void CreateAvailableStates()
    {
        EnemyAttack enemyAttack = new EnemyAttack(this, CreateSkillWeights(_attackSkills));

        if (_defenceSkills.Count > 0)
        {
            EnemyDefence enemyDefence = new EnemyDefence(this, CreateSkillWeights(_defenceSkills));

            enemyAttack.AddTransition(EnemyBattleStateKey.TryDefence, enemyDefence);
            enemyDefence.AddTransition(EnemyBattleStateKey.TryAttack, enemyAttack);
        }

        _stateMachine = new StateMachine<EnemyBattleStateKey>(enemyAttack);
    }

    private IEnumerator EnergyRegenRoutine()
    {
        while (true)
        {
            Energy.ChangeResource(_energyRegen);

            yield return _waitForEnergyRegenDelay;
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
