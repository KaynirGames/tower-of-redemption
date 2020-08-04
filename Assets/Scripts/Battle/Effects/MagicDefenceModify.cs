using UnityEngine;

[CreateAssetMenu(fileName = "NewMagicDefenceModify", menuName = "Scriptable Objects/Battle/Effects/Magic Defence Modify")]
public class MagicDefenceModify : Effect
{
    [Header("Параметры модификации магической защиты:")]
    [SerializeField] private float _magicDefenceValue = 0f; // Значение магической защиты.

    public override void Apply(CharacterStats target)
    {
        target.MagicDefence.AddModifier(new StatModifier(_magicDefenceValue, this));

        if (_hasDuration) target.StartCoroutine(RemoveEffectRoutine(target, _duration));
    }

    public override string GetDescription()
    {
        if (_hasDuration)
        {
            return string.Format(
                "{0:+0;-#} МЗАЩ ({1} сек / {2})",
                _magicDefenceValue, _duration, GameDictionary.TargetTypeNames[_targetType]);
        }

        return string.Format(
            "{0:+0;-#} МЗАЩ ({1})",
            _magicDefenceValue, GameDictionary.TargetTypeNames[_targetType]);
    }

    public override void Remove(CharacterStats target)
    {
        target.MagicDefence.RemoveSourceModifiers(this);
    }
}
