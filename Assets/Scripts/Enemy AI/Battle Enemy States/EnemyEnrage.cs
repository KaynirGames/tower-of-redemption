using KaynirGames.AI;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnrage : BaseState<EnemyBattleStateKey>
{
    private EnemyBattleAI _enemyBattleAI;
    private List<Skill> _specialSkills;

    private EnemyCharacter _enemy;
    private Skill _selectedSkill;

    private int _requiredEnergy;

    public EnemyEnrage(EnemyBattleAI battleAI, List<Skill> specialSkills)
    {
        _enemyBattleAI = battleAI;
        _specialSkills = specialSkills;

        _enemy = _enemyBattleAI.GetComponent<EnemyCharacter>();
    }

    public override void EnterState()
    {
        int random = Random.Range(0, _specialSkills.Count - 1);
        _selectedSkill = _specialSkills[random];
        _requiredEnergy = _selectedSkill.SkillSO.Cost;
    }

    public override void ExitState()
    {
        _selectedSkill = null;
        _requiredEnergy = 0;
    }

    public override BaseState<EnemyBattleStateKey> UpdateState()
    {
        if (_selectedSkill != null)
        {
            if (_enemyBattleAI.Energy.CurrentValue >= _requiredEnergy)
            {
                _selectedSkill.TryExecute(_enemy);
                _enemyBattleAI.SetTransition(EnemyBattleStateKey.TryAttack);
            }
        }

        return null;
    }
}
