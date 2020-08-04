using UnityEngine;

/// <summary>
/// Чистый магический урон, игнорирует магическую защиту.
/// </summary>
[CreateAssetMenu(fileName = "NewPureMagicDamage", menuName = "Scriptable Objects/Battle/Damages/Pure Magic Damage")]
public class PureMagicDamage : Damage
{
    [SerializeField] private int _minDamage = 0; // Минимальный урон.
    [SerializeField] private int _maxDamage = 0; // Максимальный урон.
    [SerializeField] private ElementType _magicElement = ElementType.Fire; // Стихийный элемент урона.

    public override float CalculateDamage(CharacterStats target)
    {
        float damage = Random.Range(_minDamage, _maxDamage);
        ElementEfficacy targetEfficacy = target.GetElementEfficacy(_magicElement);
        float damageTaken = damage * (targetEfficacy.EfficacyRate / 100f);

        return Mathf.Round(damageTaken);
    }

    public override string GetDescription()
    {
        return string.Format("{0}-{1} {2} (Чистый)",
            _minDamage, _maxDamage, GameDictionary.ElementTypeNames[_magicElement]);
    }
}
