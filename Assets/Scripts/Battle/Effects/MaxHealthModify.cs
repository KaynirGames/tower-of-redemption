using UnityEngine;

[CreateAssetMenu(fileName = "NewMaxHealthModify", menuName = "Scriptable Objects/Battle/Effects/Max Health Modify")]
public class MaxHealthModify : Effect
{
    [Header("Параметры модификации максимального здоровья:")]
    [SerializeField] private float _maxHealthValue = 0f; // Значение максимального здоровья.

    public override void Apply(CharacterStats target)
    {
        target.MaxHealth.AddModifier(new StatModifier(_maxHealthValue, this));

        if (_hasDuration) target.StartCoroutine(RemoveEffectRoutine(target, _duration));
    }

    public override string GetDescription()
    {
        if (_hasDuration)
        {
            return string.Format(
                "{0:+0;-#} Макс. ОЗ ({1} сек / {2})",
                _maxHealthValue, _duration, GameDictionary.TargetTypeNames[_targetType]);
        }

        return string.Format(
            "{0:+0;-#} Макс. ОЗ ({1})",
            _maxHealthValue, GameDictionary.TargetTypeNames[_targetType]);
    }

    public override void Remove(CharacterStats target)
    {
        target.MaxHealth.RemoveSourceModifiers(this);
    }
}
