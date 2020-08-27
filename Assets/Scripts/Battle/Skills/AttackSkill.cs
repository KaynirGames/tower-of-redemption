using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackSkill", menuName = "Scriptable Objects/Battle/Skills/Attack Skill")]
public class AttackSkill : Skill
{
    [Header("Параметры атакующего умения:")]
    [SerializeField] private DamageType[] _damageTypes = null;

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
        damageList.ForEach(damage => opponent.Stats.TakeDamage(damage));
    }

    public override void Deactivate(Character owner, Character opponent) { }

    public override StringBuilder GetParamsDescription()
    {
        _stringBuilder.Clear();

        _stringBuilder.Append(GameTexts.Instance.DamageTypeLabel);
        _stringBuilder.Append(": ");
        _stringBuilder.Append(_damageTypes[0].Name);

        for (int i = 1; i < _damageTypes.Length; i++)
        {
            _stringBuilder.Append(" / ");
            _stringBuilder.Append(_damageTypes[i].Name);
        }

        _stringBuilder.AppendLine().AppendLine();

        _ownerEffects.ForEach(effect => _stringBuilder.AppendLine(effect.GetDescription(TargetType.Self)));
        _opponentEffects.ForEach(effect => _stringBuilder.AppendLine(effect.GetDescription(TargetType.Opponent)));

        return _stringBuilder;
    }
}
