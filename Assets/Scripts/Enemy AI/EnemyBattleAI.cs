using KaynirGames.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleAI : MonoBehaviour
{
    public CharacterResource Health { get; private set; }
    public CharacterResource Spirit { get; private set; }

    public Skill NextSkill { get; private set; }

    private StateMachine<EnemyBattleStateKey> _stateMachine;
    private EnemyCharacter _enemy;
    private List<Skill> _avaliableSkills;

    private float _spiritRegen;
    private float _spiritRegenDelay;
    private float _spiritRegenTimer;

    private bool _enableSpiritRegen;

    private Dictionary<BattleAction, float> _battleActionRates;

    private void Awake()
    {
        _enemy = GetComponent<EnemyCharacter>();
    }

    private void Update()
    {
        _stateMachine.Update();

        if (_enableSpiritRegen)
        {
            HandleSpiritRegen();
        }
    }

    public void InitializeBattleAI()
    {
        Health = _enemy.Stats.Health;
        Spirit = _enemy.Stats.Spirit;

        _spiritRegen = _enemy.EnemySpec.SpiritRegen;
        _spiritRegenDelay = _enemy.EnemySpec.SpiritRegenDelay;
        _spiritRegenTimer = _spiritRegenDelay;

        _avaliableSkills = CollectSkills(_enemy.SkillBook);

        CreateAvailableStates();
    }

    public void ToggleSpiritRegen(bool enable)
    {
        _enableSpiritRegen = enable;
    }

    public void SetTransition(EnemyBattleStateKey battleStateKey)
    {
        _stateMachine.TransitionNext(battleStateKey);
    }

    public Dictionary<Skill, float> CalculateSkillWeights()
    {
        CalculateBattleActionRates();

        Dictionary<Skill, float> weights = new Dictionary<Skill, float>();

        foreach (Skill skill in _avaliableSkills)
        {
            if (skill.IsCooldown)
            {
                weights.Add(skill, 0);
            }
            else
            {
                weights.Add(skill, GetSkillWeight(skill));
            }
        }

        return weights;
    }

    public void SetNextSkill(Skill nextSkill)
    {
        NextSkill = nextSkill;
    }

    private float GetSkillWeight(Skill skill)
    {
        float baseWeight = _spiritRegen
                           * skill.SkillSO.Cooldown
                           + Spirit.CurrentValue
                           - skill.SkillSO.Cost;

        float battleActionRate = _battleActionRates[skill.SkillSO.BattleAction];

        return baseWeight * battleActionRate;
    }

    private void CalculateBattleActionRates()
    {
        CharacterResource playerHealth = PlayerCharacter.Active.Stats.Health;

        float playerHealthRate = playerHealth.MaxValue.GetFinalValue() / playerHealth.CurrentValue;
        float enemyHealthRate = Health.MaxValue.GetFinalValue() / Health.CurrentValue;

        _battleActionRates = new Dictionary<BattleAction, float>()
        { 
            { BattleAction.Attack, playerHealthRate},
            { BattleAction.Defence, 1 - playerHealthRate + enemyHealthRate},
            { BattleAction.Heal, enemyHealthRate}
        };
    }

    private List<Skill> CollectSkills(SkillBook skillBook)
    {
        List<Skill> skills = new List<Skill>();

        foreach (SkillSlot slot in Enum.GetValues(typeof(SkillSlot)))
        {
            if (slot == SkillSlot.Passive) { continue; }

            Skill[] slots = skillBook.GetSkillSlots(slot);

            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i] != null)
                {
                    skills.Add(slots[i]);
                }
            }
        }

        return skills;
    }

    private void CreateAvailableStates()
    {
        EnemyAct enemyAct = new EnemyAct(this);
        EnemyDecide enemyDecide = new EnemyDecide(this);

        enemyAct.AddTransition(EnemyBattleStateKey.Decide, enemyDecide);
        enemyDecide.AddTransition(EnemyBattleStateKey.Act, enemyAct);

        _stateMachine = new StateMachine<EnemyBattleStateKey>(enemyDecide);
    }

    private void HandleSpiritRegen()
    {
        if (_spiritRegenTimer <= 0)
        {
            Spirit.ChangeResource(_spiritRegen);
            _spiritRegenTimer += _spiritRegenDelay;
        }
        else
        {
            _spiritRegenTimer -= Time.deltaTime;
        }
    }
}
