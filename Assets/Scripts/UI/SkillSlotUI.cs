using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Слот умения в книге на UI.
/// </summary>
public class SkillSlotUI : MonoBehaviour
{
    public delegate void OnSkillDescriptionRequest(Skill skill);

    public static event OnSkillDescriptionRequest OnDescriptionPanelRequest = delegate { };
    public static event OnSkillDescriptionRequest OnTooltipRequest = delegate { };

    [SerializeField] private SkillSlotType _slotType = SkillSlotType.ActiveSlot;
    [SerializeField] private Image _skillIcon = null;
    [SerializeField] private Button _useButton = null;

    private SkillSlot _skillSlot;

    public void InitSlot(SkillSlot skillSlot)
    {
        if (skillSlot.SlotType == _slotType)
        {
            _skillSlot = skillSlot;
            _skillSlot.OnSkillCooldownToggle += ToggleSkillCooldownDisplay;
            // Подписка на нехватку энергии => вызов предупреждения в бою.
            UpdateSlotDisplayUI();
        }
    }

    public void UpdateSlotDisplayUI()
    {
        if (_skillSlot.IsEmpty)
        {
            ClearSlotUI();
        }
        else
        {
            FillSlotUI();
        }
    }

    public void ShowSkillDescription()
    {
        OnDescriptionPanelRequest.Invoke(_skillSlot.Skill);
    }

    public void ShowSkillTooltip()
    {
        OnTooltipRequest.Invoke(_skillSlot.Skill);
    }

    public void ActivateSkill()
    {
        _skillSlot.TryActivateSkill();
    }

    private void FillSlotUI()
    {
        _skillIcon.sprite = _skillSlot.Skill.Icon;
        _skillIcon.enabled = true;
        _useButton.interactable = true;
    }

    private void ClearSlotUI()
    {
        _skillIcon.sprite = null;
        _skillIcon.enabled = false;
        _useButton.interactable = false;
    }

    private void ToggleSkillCooldownDisplay(bool isCooldown)
    {
        _useButton.interactable = !isCooldown;
        // Анимация перезарядки?
    }
}
