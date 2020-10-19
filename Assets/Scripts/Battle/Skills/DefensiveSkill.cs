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

        _recoveryTypes.ForEach(recovery => recovery.ApplyRecovery(owner));

        _ownerEffects.ForEach(effect => effect.Apply(owner, skillInstance));
        _opponentEffects.ForEach(effect => effect.Apply(opponent, skillInstance));
    }

    public override void Terminate(Character owner, Character opponent, SkillInstance skillInstance) { }

    public override string BuildDescription()
    {
        StringBuilder builder = new StringBuilder();

        if (_recoveryTypes.Count > 0)
        {
            foreach (RecoveryType recovery in _recoveryTypes)
            {
                builder.Append(recovery.GetDescription()).Append(" ");
            }

            builder.AppendLine().AppendLine();
        }

        builder.Append(BuildEffectsDescription());

        builder.AppendLine(_flavorText.Value);

        return builder.ToString();
    }

    protected override void OnValidate()
    {
        base.OnValidate();

        _ownerEffects.RemoveAll(effect => effect.GetType() == typeof(StatBonus));
        _opponentEffects.RemoveAll(effect => effect.GetType() == typeof(StatBonus));
    }
}
