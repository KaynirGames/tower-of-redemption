using UnityEngine;

/// <summary>
/// Обычный магический урон.
/// </summary>
[CreateAssetMenu(fileName = "NewMagicDamage", menuName = "Scriptable Objects/Battle/Damages/Magic Damage")]
public class MagicDamage : Damage
{
    [SerializeField] private int _minDamage = 0; // Минимальный урон.
    [SerializeField] private int _maxDamage = 0; // Максимальный урон.
    [SerializeField] private ElementType _magicElement = ElementType.Fire; // Стихийный элемент урона.

    public override float CalculateDamage(CharacterStats target)
    {
        float damage = Random.Range(_minDamage, _maxDamage);
        ElementEfficacy targetEfficacy = target.GetElementEfficacy(_magicElement);
        float damageTaken = damage * (1 - target.MagicDefence.GetValue() / 100f) * (targetEfficacy.EfficacyRate / 100f);

        return Mathf.Round(damageTaken);
    }

    public override string GetDescription()
    {
        return string.Format("{0}-{1} {2}",
            _minDamage, _maxDamage, GameDictionary.ElementTypeNames[_magicElement]);
    }
}
