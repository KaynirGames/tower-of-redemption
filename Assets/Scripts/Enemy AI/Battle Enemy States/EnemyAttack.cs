using KaynirGames.AI;
using System.Collections.Generic;

public class EnemyAttack : BaseState<EnemyBattleStateKey>
{
    private EnemyBattleAI _enemyBattleAI;
    private Dictionary<Skill, int> _attackSkillWeights;

    private EnemyCharacter _enemy;
    private Skill _selectedSkill;

    private int _requiredEnergy;

    public EnemyAttack(EnemyBattleAI battleAI, Dictionary<Skill, int> attackSkillWeights)
    {
        _enemyBattleAI = battleAI;
        _attackSkillWeights = attackSkillWeights;

        _enemy = _enemyBattleAI.GetComponent<EnemyCharacter>();
    }

    public override void EnterState()
    {
        _selectedSkill = _enemyBattleAI.GetSuitableSkill(_attackSkillWeights);

        if (_selectedSkill == null)
        {
            _enemyBattleAI.SetTransition(EnemyBattleStateKey.TryDefence);
        }
        else
        {
            _requiredEnergy = _selectedSkill.SkillSO.Cost;
        }
    }

    public override void ExitState()
    {
        _selectedSkill = null;
        _requiredEnergy = 0;
    }

    public override BaseState<EnemyBattleStateKey> UpdateState()
    {
        if (_selectedSkill == null) { return this; }

        if (_enemyBattleAI.Energy.CurrentValue >= _requiredEnergy)
        {
            _selectedSkill.TryExecute(_enemy);
            _attackSkillWeights[_selectedSkill] += _requiredEnergy;
            return this;
        }

        return null;
    }
}
