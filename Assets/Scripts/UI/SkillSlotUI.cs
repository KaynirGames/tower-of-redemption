using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Слот умения в книге на UI.
/// </summary>
public class SkillSlotUI : MonoBehaviour
{
    public delegate void OnSkillDescriptionCall(Skill skill); // Делегат для вызова описания умения.
    public delegate void OnSkillActivationCall(Skill skill); // Делегат для вызова активации умения.
    /// <summary>
    /// Событие при вызове описания умения в инвентаре игрока.
    /// </summary>
    public static event OnSkillDescriptionCall OnInventoryDescriptionCall = delegate { };
    /// <summary>
    /// Событие при вызове описания умения во всплывающем окне.
    /// </summary>
    public static event OnSkillDescriptionCall OnTooltipDescriptionCall = delegate { };
    /// <summary>
    /// Событие при активации умения.
    /// </summary>
    public static event OnSkillActivationCall OnPlayerActivationCall = delegate { };

    [SerializeField] private SkillSlotType _slotType = SkillSlotType.ActiveSlot; // Тип слота умения.
    [SerializeField] private Image _icon = null; // Для отображения иконки умения.
    [SerializeField] private Button _useButton = null; // Кнопка для использования слота.

    private SkillSlot _skillSlot; // Соответствующий слот в книге умений.
    /// <summary>
    /// Инициализировать слот умения на UI.
    /// </summary>
    public void InitSlot(SkillSlot skillSlot)
    {
        if (skillSlot.SlotType == _slotType)
        {
            _skillSlot = skillSlot;
            UpdateSlotUI();
        }
    }
    /// <summary>
    /// Обновить отображение слота на UI.
    /// </summary>
    public void UpdateSlotUI()
    {
        if (!_skillSlot.IsEmpty)
        {
            _icon.sprite = _skillSlot.Skill.Icon;
            _icon.enabled = true;
            _useButton.interactable = true;
        }
        else
        {
            ResetSlotUI();
        }
    }
    /// <summary>
    /// Вызвать описание умения в инвентаре игрока.
    /// </summary>
    public void CallDescriptionInventory()
    {
        OnInventoryDescriptionCall.Invoke(_skillSlot.Skill);
    }
    /// <summary>
    /// Вызвать описание умения во всплывающем окне.
    /// </summary>
    public void CallDescriptionTooltip()
    {
        OnTooltipDescriptionCall.Invoke(_skillSlot.Skill);
    }
    /// <summary>
    /// Вызвать активацию умения.
    /// </summary>
    public void CallSkillActivation()
    {
        StartCoroutine(SkillCooldownRoutine(_skillSlot.Skill.Cooldown));
        OnPlayerActivationCall.Invoke(_skillSlot.Skill);
    }
    /// <summary>
    /// Сбросить отображение слота на UI.
    /// </summary>
    private void ResetSlotUI()
    {
        _icon.sprite = null;
        _icon.enabled = false;
        _useButton.interactable = false;
    }
    /// <summary>
    /// Корутина перезарядки умения.
    /// </summary>
    private IEnumerator SkillCooldownRoutine(float cooldown)
    {
        _useButton.interactable = false;
        // Анимация перезарядки?
        yield return new WaitForSecondsRealtime(cooldown);
        _useButton.interactable = true;
    }
}
