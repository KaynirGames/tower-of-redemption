using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackSkill", menuName = "Scriptable Objects/Battle/Skills/Attack Skill")]
public class AttackSkill : Skill
{
    [Header("Параметры атакующего умения:")]
    [SerializeField] private DamageType[] _damageTypes = null;

    public override void Activate(CharacterStats user, CharacterStats target)
    {
        foreach (DamageType damageType in _damageTypes)
        {
            float damage = damageType.CalculateDamage(user, target, _powerTier);
            target.TakeDamage(damage);
        }

        foreach (SkillEffect effect in _userEffects)
        {
            effect.Apply(user);
        }

        foreach (SkillEffect effect in _enemyEffects)
        {
            effect.Apply(target);
        }
    }

    public override void Deactivate(CharacterStats user, CharacterStats target) { }

    public override string GetDescription()
    {
        string descriptionFull = string.Empty;

        if (_damageTypes.Length > 0)
        {
            descriptionFull = string.Format("Урон: {0}", _damageTypes[0].Name);

            for (int i = 1; i < _damageTypes.Length; i++)
            {
                descriptionFull += string.Format(" / {0}", _damageTypes[i].Name);
            }
        }

        descriptionFull += string.Format("\nСтоимость: {0} ДЭ", _cost);
        descriptionFull += string.Format("\nПерезарядка: {0} сек", _cooldown);

        //foreach (SkillEffect effect in _userEffects)
        //{
        //    descriptionFull += string.Format("\n{0} / {1})",
        //        effect.GetDescription(), GameDictionary.TargetTypeNames[TargetType.Self]);
        //}

        //foreach (SkillEffect effect in _enemyEffects)
        //{
        //    descriptionFull += string.Format("\n{0} / {1})",
        //        effect.GetDescription(), GameDictionary.TargetTypeNames[TargetType.Enemy]);
        //}

        descriptionFull += "\n" + _description;

        return descriptionFull;
    }
}
