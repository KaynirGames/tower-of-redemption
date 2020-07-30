using UnityEngine;

[CreateAssetMenu(fileName = "NewDamageEffect", menuName = "Scriptable Objects/Battle/Effects/Damage Effect")]
public class DamageEffect : Effect
{
    [SerializeField] private float _minDamage = 0; // Минимальная величина урона.
    [SerializeField] private float _maxDamage = 0; // Максимальная величина урона.
    [SerializeField] private DamageType _damageType = null; // Тип урона.

    public override void Apply(CharacterStats target)
    {
        float damage = Random.Range(_minDamage, _maxDamage);
        float damageTaken = _damageType.CalculateDamage(damage, target);

        target.TakeDamage(damageTaken);
    }

    public override string GetDisplayInfo()
    {
        string displayInfo = string.Concat(
            "Урон: ", _minDamage, "-", _maxDamage, "\n",
            "Тип урона: ", _damageType.Name
            );
        return displayInfo;
    }
}
