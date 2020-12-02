using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Undefined Skill SO", menuName = "Scriptable Objects/Battle/Skills/Defence Skill SO")]
public class DefenceSkillSO : SkillSO
{
    [Header("Параметры защитного умения:")]
    [SerializeField] private List<RecoverySO> _recoveryTypes = null;

    public override void Execute(Character owner, Character opponent, Skill skill)
    {
        owner.Stats.ChangeSpirit(-_cost);

        _recoveryTypes.ForEach(recovery => recovery.ApplyRecovery(owner));

        _ownerEffects.ForEach(effect => effect.Apply(owner, skill));
        _opponentEffects.ForEach(effect => effect.Apply(opponent, skill));
    }

    public override void Terminate(Character owner, Character opponent, Skill skill) { }
}
