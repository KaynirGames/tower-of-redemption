using UnityEngine;

/// <summary>
/// Модификация силы.
/// </summary>
[CreateAssetMenu(fileName = "STR_Amount_Duration_ApplyMethod", menuName = "Scriptable Objects/Battle/Skill Effects/Strength Modify")]
public class StrengthModify : SkillEffect
{
    [SerializeField] private float _modifierValue = 0f;
    [SerializeField] private ModifierType _modifierType = ModifierType.Flat;
    [SerializeField] private TranslatedText _statShrinkName = null; // Перевод сокращенного названия стата.

    public override void Apply(CharacterStats target)
    {
        _applyMethod.Handle(target, this);
        target.Strength.AddModifier(new StatModifier(_modifierValue, _modifierType, this));
        _durationType.Execute(this, target);
    }

    public override void Remove(CharacterStats target)
    {
        target.Strength.RemoveSourceModifiers(this);
        _durationType.Terminate(this, target);
    }

    public override string GetDescription(TargetType targetType)
    {
        _stringBuilder.Clear();

        if (_modifierValue > 0) _stringBuilder.Append("+ ");

        _stringBuilder.Append(_modifierValue);

        if (_modifierType == ModifierType.Flat)
        { 
            _stringBuilder.Append(" ");
        }
        else { _stringBuilder.Append("% "); }

        _stringBuilder.Append(_statShrinkName.Value);
        _stringBuilder.Append(" (");

        if (targetType == TargetType.Self)
        {
            _stringBuilder.Append(GameTexts.Instance.TargetSelfLabel);
        }
        else { _stringBuilder.Append(GameTexts.Instance.TargetEnemyLabel); }
        _stringBuilder.Append(" / ");
        _stringBuilder.Append(_durationType.TypeName);
        _stringBuilder.Append(" / ");
        _stringBuilder.Append(_applyMethod.MethodName);
        _stringBuilder.Append(")");

        return _stringBuilder.ToString();
    }
}
