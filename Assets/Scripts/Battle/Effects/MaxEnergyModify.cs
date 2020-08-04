using UnityEngine;

[CreateAssetMenu(fileName = "NewMaxEnergyModify", menuName = "Scriptable Objects/Battle/Effects/Max Energy Modify")]
public class MaxEnergyModify : Effect
{
    [Header("Параметры модификации максимальной энергии:")]
    [SerializeField] private float _maxEnergyValue = 0f; // Значение максимальной энергии.

    public override void Apply(CharacterStats target)
    {
        target.MaxEnergy.AddModifier(new StatModifier(_maxEnergyValue, this));

        if (_hasDuration) target.StartCoroutine(RemoveEffectRoutine(target, _duration));
    }

    public override string GetDescription()
    {
        if (_hasDuration)
        {
            return string.Format(
                "{0:+0;-#} Макс. ОЭН ({1} сек / {2})",
                _maxEnergyValue, _duration, GameDictionary.TargetTypeNames[_targetType]);
        }

        return string.Format(
            "{0:+0;-#} Макс. ОЭН ({1})",
            _maxEnergyValue, GameDictionary.TargetTypeNames[_targetType]);
    }

    public override void Remove(CharacterStats target)
    {
        target.MaxEnergy.RemoveSourceModifiers(this);
    }
}