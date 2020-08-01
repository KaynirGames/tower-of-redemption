using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStatModify", menuName = "Scriptable Objects/Battle/Effects/Stat Modify")]
public class StatModify : Effect
{
    [Header("Параметры модификации стата:")]
    [SerializeField] private StatType _statType = StatType.MaxHealth; // Тип стата.
    [SerializeField] private string _statDisplayName = "Undefined"; // Отображаемое имя стата.
    [SerializeField] private StatModifier _statModifier = null; // Модификатор стата.
    [SerializeField] private bool _hasDuration = false; // Наличие времени действия.
    [SerializeField] private float _duration = 0f; // Время действия.

    public override void ApplyEffect(CharacterStats target)
    {
        Stat stat = target.GetStat(_statType);
        stat.AddModifier(_statModifier);

        if (_hasDuration)
        {
            target.StartCoroutine(RemoveEffectRoutine(target, _duration));
        }
    }

    public override string GetDisplayInfo()
    {
        if (_hasDuration)
        {
            return string.Format("{0:+0;-#} {1} на {2} сек.",
                _statModifier.Value, _statDisplayName, _duration);
        } else
        {
            return string.Format("{0:+0;-#} {1}.",
                _statModifier.Value, _statDisplayName);
        }
    }
    /// <summary>
    /// Убрать эффект.
    /// </summary>
    public void RemoveEffect(CharacterStats target)
    {
        Stat stat = target.GetStat(_statType);
        stat.RemoveModifier(_statModifier);
    }
    /// <summary>
    /// Корутина времени действия эффекта.
    /// </summary>
    private IEnumerator RemoveEffectRoutine(CharacterStats target, float duration)
    {
        yield return new WaitForSeconds(duration);
        RemoveEffect(target);
    }
}