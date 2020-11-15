using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Undefined Skill SO", menuName = "Scriptable Objects/Battle/Skills/Passive Skill SO")]
public class PassiveSkillSO : SkillSO
{
    [Header("Параметры пассивного умения:")]
    [SerializeField] private List<StatBonusSO> _ownerBonuses = new List<StatBonusSO>();

    public override void Execute(Character owner, Character opponent, Skill skill)
    {
        _ownerEffects.ForEach(effect => effect.Apply(owner, skill));
        _opponentEffects.ForEach(effect => effect.Apply(opponent, skill));
    }

    public override void Terminate(Character owner, Character opponent, Skill skill)
    {
        _ownerEffects.ForEach(effect => effect.Remove(owner, skill));
        _opponentEffects.ForEach(effect => effect.Remove(opponent, skill));
    }

    public void ApplyPermanentEffects(Character owner, Skill skill)
    {
        _ownerBonuses.ForEach(bonus => bonus.Apply(owner, skill));
    }

    public void RemovePermanentEffects(Character owner, Skill skill)
    {
        _ownerBonuses.ForEach(bonus => bonus.Remove(owner, skill));
    }
}
