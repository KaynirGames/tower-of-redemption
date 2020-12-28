using KaynirGames.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleAI : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _healthRateForEnrage = 0.2f;

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
    private bool _canEnrage;

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

        if (_canEnrage)
        {
            if (Health.CurrentValue < _healthRateForEnrage * Health.MaxValue.GetFinalValue())
            {
                _canEnrage = false;
                _stateMachine.TransitionNext(EnemyBattleStateKey.TryEnrage);
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
        Skill skill = null;
        int minWeight = int.MaxValue;

        foreach (var pair in skillWeights)
        {
            if (!pair.Key.IsCooldown)
            {
                if (pair.Value <= minWeight)
                {
                    minWeight = pair.Value;
                    skill = pair.Key;
                }
            }
        }

        return skill;
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
            if (skill != null)
            {
                _specialSkills.Add(skill);
            }
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

        if (_specialSkills.Count > 0)
        {
            EnemyEnrage enemyEnrage = new EnemyEnrage(this, _specialSkills);
            _canEnrage = true;

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

        foreach (Skill skill in skills)
        {
            int energyPerSecond = Mathf.RoundToInt(_energyRegen / _energyRegenDelay);
            int weight = skill.SkillSO.Cooldown * energyPerSecond - skill.SkillSO.Cost;

            skillWeights.Add(skill, weight);
        }

        return skillWeights;
    }
}
