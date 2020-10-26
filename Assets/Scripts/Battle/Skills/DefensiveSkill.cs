using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Undefined Defensive", menuName = "Scriptable Objects/Battle/Skills/Defensive Skill")]
public class DefensiveSkill : Skill
{
    [Header("Параметры защитного умения:")]
    [SerializeField] private List<RecoveryType> _recoveryTypes = null;

    public override void Execute(Character owner, Character opponent, SkillInstance skillInstance)
    {
        owner.Stats.ChangeEnergy(-_cost);
        owner.Animations.PlayDefenceClip();

        _recoveryTypes.ForEach(recovery => recovery.ApplyRecovery(owner));

        _ownerEffects.ForEach(effect => effect.Apply(owner, skillInstance));
        _opponentEffects.ForEach(effect => effect.Apply(opponent, skillInstance));
    }

    public override void Terminate(Character owner, Character opponent, SkillInstance skillInstance) { }

    public override string BuildDescription()
    {
        return string.Format(_skillData.DescriptionFormat,
                             BuildRecoveryDescription(),
                             BuildEffectsDescription(),
                             _cost,
                             _cooldown,
                             _flavorText.Value);
    }

    private string BuildRecoveryDescription()
    {
        StringBuilder builder = new StringBuilder();

        if (_recoveryTypes.Count > 0)
        {
            foreach (RecoveryType recovery in _recoveryTypes)
            {
                builder.Append(recovery.GetDescription()).Append(" ");
            }

            builder.AppendLine();
        }

        return builder.ToString();
    }

    protected override void OnValidate()
    {
        base.OnValidate();

        _ownerEffects.RemoveAll(effect => effect.GetType() == typeof(StatBonus));
        _opponentEffects.RemoveAll(effect => effect.GetType() == typeof(StatBonus));
    }
}
