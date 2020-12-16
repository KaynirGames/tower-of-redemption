using KaynirGames.AI;
using System.Collections.Generic;

public class EnemyEnrage : BaseState<EnemyBattleStateKey>
{
    private EnemyBattleAI _enemyBattleAI;
    private Dictionary<Skill, int> _specialSkillWeights;

    private EnemyCharacter _enemy;
    private Skill _selectedSkill;

    private int _requiredEnergy;

    public EnemyEnrage(EnemyBattleAI battleAI, Dictionary<Skill, int> specialSkillWeights)
    {
        _enemyBattleAI = battleAI;
        _specialSkillWeights = specialSkillWeights;

        _enemy = _enemyBattleAI.GetComponent<EnemyCharacter>();
    }

    public override void EnterState()
    {
        _selectedSkill = _enemyBattleAI.GetSuitableSkill(_specialSkillWeights);

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
            _specialSkillWeights[_selectedSkill] += _requiredEnergy;
            return this;
        }

        return null;
    }
}
