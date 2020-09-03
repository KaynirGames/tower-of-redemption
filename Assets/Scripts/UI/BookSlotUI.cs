using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Слот книги умений на UI.
/// </summary>
public class BookSlotUI : MonoBehaviour
{
    public delegate void OnSkillDescriptionRequest(Skill skill);

    public static event OnSkillDescriptionRequest OnDescriptionPanelRequest = delegate { };
    public static event OnSkillDescriptionRequest OnTooltipRequest = delegate { };

    [SerializeField] private BookSlotType _slotType = BookSlotType.Active;
    [SerializeField] private Image _skillIcon = null;
    [SerializeField] private Button _useButton = null;

    public BookSlot BookSlot { get; private set; }

    public BookSlotType SlotType => _slotType;

    public void InitSlot(BookSlot bookSlot)
    {
        if (bookSlot.SlotType == _slotType)
        {
            BookSlot = bookSlot;
            BookSlot.OnSkillCooldownToggle += ToggleSkillCooldownDisplay;
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
        OnDescriptionPanelRequest.Invoke(BookSlot.Skill);
    }

    public void ShowSkillTooltip()
    {
        OnTooltipRequest.Invoke(BookSlot.Skill);
    }

    public void ActivateSkill()
    {
        BookSlot.TryActivateSkill();
    }

    private void FillSlotUI()
    {
        _skillIcon.sprite = BookSlot.Skill.Icon;
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
