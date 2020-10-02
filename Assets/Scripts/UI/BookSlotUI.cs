using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Слот книги умений на UI.
/// </summary>
public class BookSlotUI : MonoBehaviour
{
    public static event DescriptionUI.OnDescriptionCall OnSkillDescriptionCall = delegate { };

    [SerializeField] private BookSlotType _slotType = BookSlotType.Active;
    [SerializeField] private Image _skillIconDisplay = null;
    [SerializeField] private Button _useButton = null;
    [Header("Отображение перезарядки умения:")]
    [SerializeField] private bool _displayCooldown = false;
    [SerializeField] private Image _skillCooldownDisplay = null;

    public BookSlot BookSlot { get; private set; }

    public BookSlotType SlotType => _slotType;

    private float _skillCooldown;

    public void InitSlot(BookSlot bookSlot)
    {
        if (bookSlot.SlotType == _slotType)
        {
            BookSlot = bookSlot;

            if (_displayCooldown)
            {
                BookSlot.OnCooldownToggle += ToggleCooldownDisplay;
                BookSlot.OnCooldownUpdate += UpdateCooldownDisplay;
            }
            // Подписка на нехватку энергии => вызов предупреждения в бою.
            UpdateSlotDisplayUI();
        }
    }

    public void UpdateSlotDisplayUI()
    {
        if (BookSlot.IsEmpty)
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
        string name = BookSlot.Skill.SkillName;
        string type = BookSlot.Skill.SkillType.TypeName;
        string description = BookSlot.Skill.Description;

        OnSkillDescriptionCall(name, type, description);
    }

    public void ActivateSkill()
    {
        BookSlot.TryActivateSkill();
    }

    private void FillSlotUI()
    {
        _skillIconDisplay.sprite = BookSlot.Skill.Icon;
        _skillIconDisplay.enabled = true;
        _useButton.interactable = true;
        _useButton.targetGraphic.raycastTarget = true;

        if (_displayCooldown)
        {
            _skillCooldown = BookSlot.Skill.Cooldown;
            _skillCooldownDisplay.sprite = BookSlot.Skill.Icon;
        }
    }

    private void ClearSlotUI()
    {
        _skillIconDisplay.sprite = null;
        _skillIconDisplay.enabled = false;
        _useButton.interactable = false;
        _useButton.targetGraphic.raycastTarget = false;

        if (_displayCooldown) 
        {
            _skillCooldownDisplay.sprite = null;
        }
    }

    private void ToggleCooldownDisplay(bool enable)
    {
        _useButton.interactable = !enable;
        _skillCooldownDisplay.enabled = enable;
        _skillCooldownDisplay.fillAmount = enable ? 1 : 0;
    }

    private void UpdateCooldownDisplay(float timer)
    {
        _skillCooldownDisplay.fillAmount = timer / _skillCooldown;
    }
}
