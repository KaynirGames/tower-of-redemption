using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Undefined Attack", menuName = "Scriptable Objects/Battle/Skills/Attack Skill")]
public class AttackSkill : Skill
{
    [Header("Параметры атакующего умения:")]
    [SerializeField] private DamageTier _damageTier = null;
    [SerializeField] private Damage[] _damageTypes = null;

    public override void Execute(Character owner, Character opponent, SkillInstance skillInstance)
    {
        float finalDamage = 0;

        foreach (Damage damage in _damageTypes)
        {
            finalDamage += damage.CalculateDamage(owner,
                                                  opponent,
                                                  _damageTier);
        }

        owner.Stats.ChangeEnergy(-_cost);
        opponent.Stats.ChangeHealth(-finalDamage);

        _ownerEffects.ForEach(effect => effect.Apply(owner, skillInstance));
        _opponentEffects.ForEach(effect => effect.Apply(opponent, skillInstance));
    }

    public override void Terminate(Character owner, Character opponent, SkillInstance skillInstance) { }

    public override string BuildDescription()
    {
        StringBuilder builder = new StringBuilder();

        if (_damageTypes.Length > 0)
        {
            BuildDamageDescription(builder);
            builder.AppendLine();
        }

        builder.AppendLine().Append(BuildEffectsDescription());

        builder.AppendLine(_flavorText.Value);

        return builder.ToString();
    }

    private void BuildDamageDescription(StringBuilder builder)
    {
        builder.Append(_damageTypes[0].Name);

        for (int i = 1; i < _damageTypes.Length; i++)
        {
            builder.Append(" / ")
                   .Append(_damageTypes[i].Name);
        }

        string damageTypes = builder.ToString();
        builder.Clear();

        builder.AppendFormat(_skillType.DescriptionFormat,
                             damageTypes,
                             _damageTier.TierName);
    }

    protected override void OnValidate()
    {
        base.OnValidate();

        _ownerEffects.RemoveAll(effect => effect.GetType() == typeof(StatBonus));
        _opponentEffects.RemoveAll(effect => effect.GetType() == typeof(StatBonus));
    }
}
