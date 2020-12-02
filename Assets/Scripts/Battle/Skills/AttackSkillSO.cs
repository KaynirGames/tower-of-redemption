using UnityEngine;

[CreateAssetMenu(fileName = "Undefined Skill SO", menuName = "Scriptable Objects/Battle/Skills/Attack Skill SO")]
public class AttackSkillSO : SkillSO
{
    [Header("Параметры атакующего умения:")]
    [SerializeField] private DamageSO _damageType = null;
    [SerializeField] private float _attackPower = 0.8f;

    public override void Execute(Character owner, Character opponent, Skill skill)
    {
        owner.Stats.ChangeSpirit(-_cost);
        owner.Animations.PlayAnimation("Attack");

        if (TryNullifyAttack(opponent)) { return; }

        float damage = _damageType.CalculateDamage(owner,
                                                   opponent,
                                                   _attackPower);

        _ownerEffects.ForEach(effect => effect.Apply(owner, skill));
        _opponentEffects.ForEach(effect => effect.Apply(opponent, skill));

        opponent.Stats.ChangeHealth(-damage);
    }

    public override void Terminate(Character owner, Character opponent, Skill skill) { }

    private bool TryNullifyAttack(Character character)
    {
        foreach (var ailment in character.Effects.AilmentEffects)
        {
            if (ailment.Value.EffectSO.GetType() == typeof(AttackNullifierSO))
            {
                AttackNullifierSO nullifier = (AttackNullifierSO)ailment.Value.EffectSO;

                if (nullifier.IsAttackNullable(_damageType))
                {
                    // Вопроизвести анимацию/показать текст о блокировании атаки?
                    ailment.Value.RemoveCharge();
                    return true;
                }
            }
        }

        return false;
    }
}
