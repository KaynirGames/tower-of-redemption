using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackAbility", menuName = "Scriptable Objects/Battle/Abilities/Attack Ability")]
public class AttackAbility : Ability
{
    [Header("Параметры атакующего умения:")]
    [SerializeField] private List<Damage> _damageTypes = new List<Damage>(); // Типы наносимого урона.

    public override void Activate(CharacterStats user, CharacterStats target)
    {
        foreach (Damage damage in _damageTypes)
        {
            float damageTaken = damage.CalculateDamage(target);
            target.TakeDamage(damageTaken);
        }

        foreach (Effect effect in _effects)
        {
            if (effect.TargetType == TargetType.Self)
            {
                user.AddEffect(effect);
            }
            else if (effect.TargetType == TargetType.Enemy)
            {
                target.AddEffect(effect);
            }
        }
    }

    public override string GetDescription()
    {
        string description = "";

        foreach (Damage damage in _damageTypes)
        {
            description += damage.GetDescription() + " / ";
        }

        description += string.Format("\n\n" +
            "Стоимость: {0} ОЭН / Перезарядка: {1} сек", _cost, _cooldown);

        foreach (Effect effect in _effects)
        {
            description += effect.GetDescription() + "\n";
        }

        return description;
    }
}
