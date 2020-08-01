using UnityEngine;

[CreateAssetMenu(fileName = "NewDamage", menuName = "Scriptable Objects/Battle/Effects/Damage")]
public class Damage : Effect
{
    [SerializeField] private float _minDamage = 0; // Минимальная величина урона.
    [SerializeField] private float _maxDamage = 0; // Максимальная величина урона.
    [SerializeField] private DamageType _damageType = null; // Тип урона.

    public override void ApplyEffect(CharacterStats target)
    {
        float damage = Random.Range(_minDamage, _maxDamage);
        float damageTaken = _damageType.CalculateDamage(damage, target);

        target.TakeDamage(damageTaken);
    }

    public override string GetDisplayInfo()
    {
        return string.Format("Урон: {0}-{1} {2}.",
            _minDamage, _maxDamage, _damageType.DisplayName);
    }
}
