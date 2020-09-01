using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackSkill", menuName = "Scriptable Objects/Battle/Skills/Attack Skill")]
public class AttackSkill : Skill
{
    [Header("Параметры атакующего умения:")]
    [SerializeField] private DamageType[] _damageTypes = null;
    [SerializeField] private PowerTier _powerTier = null;

    public override void Activate(Character owner, Character opponent)
    {
        List<float> damageList = new List<float>();

        // Записываем получаемый целью урон.
        foreach (DamageType damageType in _damageTypes)
        {
            damageList.Add(damageType.CalculateDamage(owner.Stats, opponent.Stats, _powerTier));
        }

        // Накладываем эффекты.
        _ownerEffects.ForEach(effect => effect.Apply(owner.Stats));
        _opponentEffects.ForEach(effect => effect.Apply(opponent.Stats));

        // Наносим рассчитанный урон.
        damageList.ForEach(damage => opponent.Stats.ChangeHealth(damage));
    }

    public override void Deactivate(Character owner, Character opponent) { }

    public override void BuildParamsDescription(StringBuilder stringBuilder)
    {
        
    }
}
