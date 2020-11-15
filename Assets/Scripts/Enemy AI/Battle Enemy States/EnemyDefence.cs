using KaynirGames.AI;
using System.Collections.Generic;

public class EnemyDefence : BaseState<EnemyBattleStateKey>
{
    private EnemyBattleAI _enemyBattleAI;
    private Dictionary<Skill, int> _defenceSkillWeights;

    private EnemyCharacter _enemy;
    private Skill _selectedSkill;

    private int _requiredEnergy;

    public EnemyDefence(EnemyBattleAI battleAI, Dictionary<Skill, int> defenceSkillWeights)
    {
        _enemyBattleAI = battleAI;
        _defenceSkillWeights = defenceSkillWeights;

        _enemy = _enemyBattleAI.GetComponent<EnemyCharacter>();
    }

    public override void EnterState()
    {
        _selectedSkill = _enemyBattleAI.GetSuitableSkill(_defenceSkillWeights);

        if (_selectedSkill == null)
        {
            _enemyBattleAI.SetTransition(EnemyBattleStateKey.TryAttack);
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
        if (_selectedSkill == null) { return null; }

        if (_enemyBattleAI.Energy.CurrentValue >= _requiredEnergy)
        {
            _selectedSkill.TryExecute(_enemy);
            _defenceSkillWeights[_selectedSkill] += _requiredEnergy;
            return this;
        }

        return null;
    }
}
