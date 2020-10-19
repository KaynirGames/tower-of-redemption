using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Слот умения на UI.
/// </summary>
public class SkillSlotUI : MonoBehaviour
{
    public static event DescriptionUI.OnDescriptionCall OnSkillDescriptionCall = delegate { };

    [SerializeField] private SkillSlot _slot = SkillSlot.Active;
    [SerializeField] private Image _skillIcon = null;
    [SerializeField] private Button _useButton = null;
    [Header("Отображение перезарядки умения:")]
    [SerializeField] private bool _displayCooldown = false;
    [SerializeField] private Image _skillCooldownMask = null;

    public SkillSlot Slot => _slot;

    private Character _owner;

    private SkillInstance _skill;
    private float _skillCooldown;

    public void RegisterSlotUI(Character owner)
    {
        _owner = owner;
    }

    public void UpdateSlotUI(SkillInstance skill)
    {
        if (skill == null)
        {
            ClearSlotUI();
        }
        else
        {
            FillSlotUI(skill);
        }
    }

    public void ShowSkillDescription()
    {
        string name = _skill.Skill.Name;
        string type = _skill.Skill.TypeName;
        string description = _skill.GetDescription();

        OnSkillDescriptionCall(name, type, description);
    }

    public void ActivateSkill()
    {
        _skill.TryExecute(_owner);
    }

    private void FillSlotUI(SkillInstance skill)
    {
        _skill = skill;
        _skillIcon.sprite = _skill.Skill.Icon;
        _skillIcon.enabled = true;
        _useButton.interactable = true;
        _useButton.targetGraphic.raycastTarget = true;

        if (_displayCooldown)
        {
            _skillCooldown = _skill.Skill.Cooldown;
            _skillCooldownMask.sprite = _skill.Skill.Icon;

            _skill.OnCooldownToggle += ToggleCooldownDisplay;
            _skill.OnCooldownTick += UpdateCooldownDisplay;
        }
    }

    private void ClearSlotUI()
    {
        _skillIcon.sprite = null;
        _skillIcon.enabled = false;
        _useButton.interactable = false;
        _useButton.targetGraphic.raycastTarget = false;

        if (_displayCooldown) 
        {
            _skillCooldownMask.sprite = null;

            if (_skill != null)
            {
                _skill.OnCooldownToggle -= ToggleCooldownDisplay;
                _skill.OnCooldownTick -= UpdateCooldownDisplay;
            }
        }

        _skill = null;
    }

    private void ToggleCooldownDisplay(bool enable)
    {
        _useButton.interactable = !enable;
        _skillCooldownMask.enabled = enable;
        _skillCooldownMask.fillAmount = enable ? 1 : 0;
    }

    private void UpdateCooldownDisplay(float timer)
    {
        _skillCooldownMask.fillAmount = timer / _skillCooldown;
    }
}