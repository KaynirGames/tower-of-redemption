using System;
using UnityEngine;

public class SkillReplaceUI : MonoBehaviour
{
    [SerializeField] private DescriptionUI _descriptionUI = null;
    [SerializeField] private SkillBookUI _skillBookUI = null;
    [SerializeField] private SkillSlotUI _newSkillSlotUI = null;

    private SkillBook _skillBook;
    private CanvasGroup _canvasGroup;

    private Action<int> _storedActionOnConfirm;
    private SkillSlotUI[] _avaliableSlots;
    private SkillSlotUI _selectedSlot;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        PlayerCharacter.OnPlayerActive += RegisterSkillBook;
    }

    public void ShowSkillReplaceWindow(SkillSO skillSO, Action<int> onConfirm)
    {
        _descriptionUI.ClearDescriptionText();
        _skillBookUI.ToggleSlotsInteraction(false);
        _selectedSlot = null;

        _newSkillSlotUI.UpdateSlotUI(new Skill(skillSO));
        _avaliableSlots = _skillBookUI.GetSlotsUI(skillSO.Slot);
        _storedActionOnConfirm = onConfirm;

        ToggleSkillReplaceWindow(true);
    }

    public void SelectSlot(SkillSlotUI skillSlotUI)
    {
        _selectedSlot = skillSlotUI;
        ShowSkillDescription();
    }

    public void ReplaceSkill(bool confirm)
    {
        if (_selectedSlot != null)
        {
            if (confirm && _selectedSlot.SlotID > -1)
            {
                _storedActionOnConfirm?.Invoke(_selectedSlot.SlotID);
                _storedActionOnConfirm = null;
            }
        }

        ToggleSkillReplaceWindow(false);
    }

    public void ShowSkillDescription()
    {
        if (_selectedSlot != null)
        {
            SkillSO skillSO = _selectedSlot.Skill.SkillSO;
            _descriptionUI.SetDescriptionText(skillSO.Name, skillSO.Type, skillSO.Description);
        }
    }

    public void ShowSkillDescription(SkillSlotUI skillSlotUI)
    {
        if (skillSlotUI != null)
        {
            SkillSO skillSO = skillSlotUI.Skill.SkillSO;
            _descriptionUI.SetDescriptionText(skillSO.Name, skillSO.Type, skillSO.Description);
        }
    }

    private void RegisterSkillBook(PlayerCharacter player)
    {
        _skillBook = player.SkillBook;
        _skillBookUI.RegisterSkillBook(_skillBook);
    }

    private void ToggleSkillReplaceWindow(bool enable)
    {
        ToggleAvaliableSlots(enable);
        _canvasGroup.alpha = enable ? 1 : 0;
        _canvasGroup.blocksRaycasts = enable;
    }

    private void ToggleAvaliableSlots(bool enable)
    {
        for (int i = 0; i < _avaliableSlots.Length; i++)
        {
            _avaliableSlots[i].ToggleSlotInteraction(enable);
        }

        if (!enable)
        {
            _avaliableSlots = null;
        }
    }

    private void OnDestroy()
    {
        PlayerCharacter.OnPlayerActive -= RegisterSkillBook;
    }
}
