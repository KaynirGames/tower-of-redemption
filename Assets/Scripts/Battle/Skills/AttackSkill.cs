using System.Collections;
using System.Collections.Generic;
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

        // Обработать наложение эффектов
    }

    public override void Deactivate(CharacterStats user, CharacterStats target)
    {
        
    }

    public override string GetDescription()
    {
        string description = string.Empty;

        return description;
    }
}
