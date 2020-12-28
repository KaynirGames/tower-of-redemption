using KaynirGames.AI;

public class EnemyDecide : BaseState<EnemyBattleStateKey>
{
    private EnemyBattleAI _battleAI;

    public EnemyDecide(EnemyBattleAI battleAI)
    {
        _battleAI = battleAI;
    }

    public override void EnterState() { }

    public override void ExitState() { }

    public override BaseState<EnemyBattleStateKey> UpdateState()
    {
        if (DecideNextSkill(out Skill nextSkill))
        {
            _battleAI.SetNextSkill(nextSkill);
            _battleAI.SetTransition(EnemyBattleStateKey.Act);
        }

        return null;
    }

    private bool DecideNextSkill(out Skill nextSkill)
    {
        float maxWeight = 0;
        nextSkill = null;

        var skillWeights = _battleAI.CalculateSkillWeights();

        foreach (var pair in skillWeights)
        {
            if (pair.Value > 0)
            {
                if (pair.Value > maxWeight)
                {
                    maxWeight = pair.Value;
                    nextSkill = pair.Key;
                }
            }
        }

        return nextSkill == null
            ? false
            : true;
    }
}
