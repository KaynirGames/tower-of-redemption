using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewOffensiveAbility", menuName = "Scriptable Objects/Abilities/Offensive Ability")]
public class OffensiveAbility : Ability
{
    [Header("Параметры наступательного умения:")]
    [SerializeField] private float _minDamage = 0; // Минимальная величина урона.
    [SerializeField] private float _maxDamage = 0; // Максимальная величина урона.
    [SerializeField] private float _energyCost = 0; // Затраты очков энергии.
    [SerializeField] private DamageType _damageType = null; // Тип урона.

    /// <summary>
    /// Затраты очков энергии.
    /// </summary>
    public float EnergyCost => _energyCost;

    public override void Activate(CharacterStats target)
    {
        float damage = Random.Range(_minDamage, _maxDamage);
        float damageTaken = _damageType.CalculateDamage(damage, target);

        target.TakeDamage(damageTaken);
    }

    public override string GetDescription()
    {
        return "";
    }
}