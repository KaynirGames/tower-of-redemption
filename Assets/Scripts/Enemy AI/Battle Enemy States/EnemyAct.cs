using KaynirGames.AI;

public class EnemyAct : BaseState<EnemyBattleStateKey>
{
    private EnemyBattleAI _battleAI;
    private EnemyCharacter _enemy;
    private float _requiredSpirit;
    private Skill _nextSkill;

    public EnemyAct(EnemyBattleAI battleAI)
    {
        _battleAI = battleAI;
        _enemy = _battleAI.GetComponent<EnemyCharacter>();
    }

    public override void EnterState()
    {
        _nextSkill = _battleAI.NextSkill;
        _requiredSpirit = _nextSkill.SkillSO.Cost;
    }

    public override void ExitState() { }

    public override BaseState<EnemyBattleStateKey> UpdateState()
    {
        if (TryExecuteNextSkill())
        {
            _battleAI.SetTransition(EnemyBattleStateKey.Decide);
        }

        return null;
    }

    private bool TryExecuteNextSkill()
    {
        return _battleAI.Spirit.CurrentValue >= _requiredSpirit
            ? _nextSkill.TryExecute(_enemy)
            : false;
    }
}
