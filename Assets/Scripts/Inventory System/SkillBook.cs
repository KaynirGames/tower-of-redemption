using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Книга умений.
/// </summary>
public class SkillBook : MonoBehaviour
{
    public delegate void OnSkillBookChange(int slotID, SkillSlotType slotType);

    public event OnSkillBookChange OnSkillSlotChange = delegate { };

    [SerializeField] private int _activeSlotsCount = 4;
    [SerializeField] private int _passiveSlotsCount = 3;
    [SerializeField] private int _specialSlotsCount = 1;

    private SkillSlot[] _activeSlots;
    private SkillSlot[] _passiveSlots;
    private SkillSlot[] _specialSlots;

    private Dictionary<SkillSlotType, SkillSlot[]> _bookSlots;

    private Character _skillBookOwner;

    private void Awake()
    {
        _skillBookOwner = GetComponent<Character>();

        _activeSlots = CreateBookSlots(_activeSlotsCount, SkillSlotType.ActiveSlot);
        _passiveSlots = CreateBookSlots(_passiveSlotsCount, SkillSlotType.PassiveSlot);
        _specialSlots = CreateBookSlots(_specialSlotsCount, SkillSlotType.SpecialSlot);

        _bookSlots = new Dictionary<SkillSlotType, SkillSlot[]>
        { 
            { SkillSlotType.ActiveSlot, _activeSlots },
            { SkillSlotType.PassiveSlot, _passiveSlots },
            { SkillSlotType.SpecialSlot, _specialSlots }
        };
    }

    public void SetBaseSpecSkills(SpecBase currentSpec)
    {
        foreach (Skill skill in currentSpec.BaseSkills)
        {
            AddSkill(skill);
        }
    }

    public void AddSkill(Skill skill)
    {
        int slotID = FindFirstEmptySlot(skill.SkillSlotType);

        if (slotID >= 0)
        {
            InsertSkill(skill, slotID);
        }
        else
        {
            // Вызвать меню для замены умения в слоте.
        }
    }

    public Skill RemoveSkill(int slotID, SkillSlotType slotType)
    {
        SkillSlot[] slots = GetBookSlots(slotType);

        Skill removedSkill = slots[slotID].Skill;

        if (slotType == SkillSlotType.PassiveSlot)
        {
            TogglePassiveSkillForOwner(removedSkill, false);
        }

        slots[slotID].RemoveSkill();

        OnSkillSlotChange.Invoke(slotID, slotType);

        return removedSkill;
    }

    public Skill ReplaceSkill(int slotID, Skill newSkill)
    {
        Skill removedSkill = RemoveSkill(slotID, newSkill.SkillSlotType);
        InsertSkill(newSkill, slotID);

        return removedSkill;
    }

    public SkillSlot[] GetBookSlots(SkillSlotType slotType)
    {
        return _bookSlots[slotType];
    }

    private SkillSlot[] CreateBookSlots(int slotAmount, SkillSlotType slotType)
    {
        SkillSlot[] slots = new SkillSlot[slotAmount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new SkillSlot(slotType, i, _skillBookOwner);
        }

        return slots;
    }

    private void InsertSkill(Skill skill, int slotID)
    {
        SkillSlot[] slots = GetBookSlots(skill.SkillSlotType);
        slots[slotID].InsertSkill(skill);

        if (skill.SkillSlotType == SkillSlotType.PassiveSlot)
        {
            TogglePassiveSkillForOwner(skill, true);
        }

        OnSkillSlotChange.Invoke(slotID, skill.SkillSlotType);
    }

    private int FindFirstEmptySlot(SkillSlotType slotType)
    {
        SkillSlot[] slots = GetBookSlots(slotType);

        for (int i = 0; i < slots.Length; i++) {
            if (slots[i].IsEmpty)
            {
                return i;
            }
        }

        return -1;
    }

    private void TogglePassiveSkillForOwner(Skill passiveSkill, bool enable)
    {
        if (enable)
        {
            passiveSkill.Activate(_skillBookOwner, null);
        }
        else
        {
            passiveSkill.Deactivate(_skillBookOwner, null);
        }
    }
}