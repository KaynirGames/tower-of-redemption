using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour
{
    /// <summary>
    /// Делегат для вызова описания умения.
    /// </summary>
    public delegate void OnSkillDescriptionCall(Skill skill);
    /// <summary>
    /// Событие, вызывающее описание умения в окне персонажа.
    /// </summary>
    public static event OnSkillDescriptionCall OnDescriptionCall = delegate { };

    [SerializeField] private SkillSlotType _slotType = SkillSlotType.ActiveSlot; // Тип слота умения.
    [SerializeField] private Image _icon = null; // Для отображения иконки умения.

    private Skill _skill; // Умение в слоте.
    /// <summary>
    /// Вставить умение в слот.
    /// </summary>
    public void SetSkill(Skill skill)
    {
        if (skill == null)
        {
            ClearSlot();
            return;
        }

        if (skill.SkillSlotType == _slotType)
        {
            _skill = skill;
            _icon.sprite = skill.Icon;
            _icon.enabled = true;
        }
    }
    /// <summary>
    /// Очистить слот умения.
    /// </summary>
    public void ClearSlot()
    {
        _skill = null;
        _icon.sprite = null;
        _icon.enabled = false;
    }
    /// <summary>
    /// Вызвать описание умения в окне персонажа.
    /// </summary>
    public void CallDescription()
    {
        if (_skill != null)
        {
            OnDescriptionCall.Invoke(_skill);
        }
    }
}
