using UnityEngine;

[CreateAssetMenu(fileName = "NewDefenceModify", menuName = "Scriptable Objects/Battle/Effects/Defence Modify")]
public class DefenceModify : Effect
{
    [Header("Параметры модификации физической защиты:")]
    [SerializeField] private float _armorValue = 0f; // Значение физической защиты.

    public override void Apply(CharacterStats target)
    {
        target.Defence.AddModifier(new StatModifier(_armorValue, this));

        if (_hasDuration) target.StartCoroutine(RemoveEffectRoutine(target, _duration));
    }

    public override string GetDescription()
    {
        if (_hasDuration)
        {
            return string.Format(
                "{0:+0;-#} ЗАЩ ({1} сек / {2})",
                _armorValue, _duration, GameDictionary.TargetTypeNames[_targetType]);
        }

        return string.Format(
            "{0:+0;-#} ЗАЩ ({1})",
            _armorValue, GameDictionary.TargetTypeNames[_targetType]);
    }

    public override void Remove(CharacterStats target)
    {
        target.Defence.RemoveSourceModifiers(this);
    }
}
