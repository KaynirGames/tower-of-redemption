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

    public int SlotID { get; private set; }
    public Skill Skill { get; private set; }

    private Character _owner;
    private float _skillCooldown;

    private void Awake()
    {
        SlotID = -1;
    }

    public void RegisterSlotUI(Character owner, int slotID)
    {
        _owner = owner;
        SlotID = slotID;
    }

    public void UpdateSlotUI(Skill skill)
    {
        ClearSlotUI();

        if (skill != null)
        {
            FillSlotUI(skill);
        }
    }

    public void ShowSkillDescription()
    {
        string name = Skill.SkillSO.Name;
        string type = Skill.SkillSO.Type;
        string description = Skill.GetDescription();

        OnSkillDescriptionCall(name, type, description);
    }

    public void ActivateSkill()
    {
        Skill.TryExecute(_owner);
    }

    public void ToggleSlotInteraction(bool enable)
    {
        _useButton.interactable = enable;
    }

    private void FillSlotUI(Skill skill)
    {
        _skillIcon.sprite = skill.SkillSO.Icon;

        ToggleSlotDisplay(true);

        if (_displayCooldown)
        {
            ToggleCooldownDisplay(false);

            _skillCooldown = skill.SkillSO.Cooldown;
            _skillCooldownMask.sprite = skill.SkillSO.Icon;

            skill.OnCooldownToggle += ToggleCooldownDisplay;
            skill.OnCooldownTick += UpdateCooldownDisplay;
        }

        Skill = skill;
    }

    private void ClearSlotUI()
    {
        ToggleSlotDisplay(false);

        if (_displayCooldown)
        {
            if (Skill != null)
            {
                Skill.OnCooldownToggle -= ToggleCooldownDisplay;
                Skill.OnCooldownTick -= UpdateCooldownDisplay;
            }

            ToggleCooldownDisplay(false);
        }

        Skill = null;
    }

    private void ToggleSlotDisplay(bool enable)
    {
        _skillIcon.enabled = enable;
        _useButton.interactable = enable;
    }

    private void ToggleCooldownDisplay(bool enable)
    {
        _skillCooldownMask.enabled = enable;
        _skillCooldownMask.fillAmount = enable ? 1 : 0;
    }

    private void UpdateCooldownDisplay(float timer)
    {
        _skillCooldownMask.fillAmount = timer / _skillCooldown;
    }
}