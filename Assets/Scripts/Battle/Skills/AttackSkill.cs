using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackSkill", menuName = "Scriptable Objects/Battle/Skills/Attack Skill")]
public class AttackSkill : Skill
{
    [Header("Параметры атакующего умения:")]
    [SerializeField] private DamageType[] _damageTypes = null; // Типы наносимого урона.

    public override void Activate(CharacterStats user, CharacterStats target)
    {
        List<float> damageList = new List<float>();

        // Записываем получаемый целью урон.
        foreach (DamageType damageType in _damageTypes)
        {
            damageList.Add(damageType.CalculateDamage(user, target, _powerTier));
        }

        // Накладываем эффекты.
        _userEffects.ForEach(effect => effect.Apply(user));
        _enemyEffects.ForEach(effect => effect.Apply(target));

        // Наносим рассчитанный урон.
        damageList.ForEach(damage => target.TakeDamage(damage));
    }

    public override void Deactivate(CharacterStats user, CharacterStats target) { }

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

        if (_userEffects.Count > 0)
        {
            _userEffects.ForEach(effect => _stringBuilder.AppendLine(effect.GetDescription(TargetType.Self)));
        }

        if (_enemyEffects.Count > 0)
        {
            _enemyEffects.ForEach(effect => _stringBuilder.AppendLine(effect.GetDescription(TargetType.Enemy)));
        }

        return _stringBuilder;
    }
}
