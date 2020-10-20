using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Undefined Attack", menuName = "Scriptable Objects/Battle/Skills/Attack Skill")]
public class AttackSkill : Skill
{
    [Header("Параметры атакующего умения:")]
    [SerializeField] private DamageTier _damageTier = null;
    [SerializeField] private Damage _damageType = null;

    public override void Execute(Character owner, Character opponent, SkillInstance skillInstance)
    {
        owner.Stats.ChangeEnergy(-_cost);

        if (IsAttackNullable(opponent)) { return; }

        float damage = _damageType.CalculateDamage(owner,
                                                   opponent,
                                                   _damageTier);

        _ownerEffects.ForEach(effect => effect.Apply(owner, skillInstance));
        _opponentEffects.ForEach(effect => effect.Apply(opponent, skillInstance));

        opponent.Stats.ChangeHealth(-damage);
    }

    public override void Terminate(Character owner, Character opponent, SkillInstance skillInstance) { }

    public override string BuildDescription()
    {
        StringBuilder builder = new StringBuilder();

        if (_damageType != null)
        {
            builder.AppendFormat(_skillData.DescriptionFormat,
                     _damageType.GetDamageName(),
                     _damageTier.TierName);

            builder.AppendLine().AppendLine();
        }

        builder.Append(BuildEffectsDescription());

        builder.AppendLine(_flavorText.Value);

        return builder.ToString();
    }

    private bool IsAttackNullable(Character character)
    {
        foreach (var ailment in character.Effects.AilmentEffects)
        {
            if (ailment.Value.Effect.GetType() == typeof(AttackNullifier))
            {
                AttackNullifier nullifier = (AttackNullifier)ailment.Value.Effect;

                if (nullifier.TryNullifyAttack(_damageType))
                {
                    // Вопроизвести анимацию/показать текст о блокировании атаки?
                    ailment.Value.RemoveCharge();
                    return true;
                }

                return false;
            }
        }

        return false;
    }

    protected override void OnValidate()
    {
        base.OnValidate();

        _ownerEffects.RemoveAll(effect => effect.GetType() == typeof(StatBonus));
        _opponentEffects.RemoveAll(effect => effect.GetType() == typeof(StatBonus));
    }
}
