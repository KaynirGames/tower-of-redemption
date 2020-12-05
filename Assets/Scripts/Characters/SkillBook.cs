using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Книга умений.
/// </summary>
public class SkillBook : MonoBehaviour
{
    public delegate void OnSkillSlotChange(int slotID, SkillSlot slot, Skill skill);

    public OnSkillSlotChange OnSlotChange = delegate { };

    [SerializeField] private int _activeSkillsCount = 4;
    [SerializeField] private int _passiveSkillsCount = 3;
    [SerializeField] private int _specialSkillsCount = 1;

    public Character Owner { get; private set; }

    private Skill[] _activeSkills;
    private Skill[] _passiveSkills;
    private Skill[] _specialSkills;

    private Dictionary<SkillSlot, Skill[]> _skillSlots;

    private void Awake()
    {
        Owner = GetComponent<Character>();

        _activeSkills = new Skill[_activeSkillsCount];
        _passiveSkills = new Skill[_passiveSkillsCount];
        _specialSkills = new Skill[_specialSkillsCount];

        _skillSlots = CreateSkillSlots();
    }

    public void LoadSkills(SkillSO[] skills)
    {
        for (int i = 0; i < skills.Length; i++)
        {
            AddSkill(skills[i], i);
        }
    }

    public Skill[] GetSkillSlots(SkillSlot slot)
    {
        return _skillSlots[slot];
    }

    public bool TryAddSkill(SkillSO skillSO)
    {
        int slotID = FindFirstEmptySlot(skillSO.Slot);

        if (slotID >= 0)
        {
            AddSkill(skillSO, slotID);
            return true;
        }

        return false;
    }

    public Skill RemoveSkill(int slotID, SkillSlot slot)
    {
        Skill[] slots = GetSkillSlots(slot);

        Skill removedSkill = slots[slotID];
        slots[slotID] = null;

        OnSlotChange.Invoke(slotID, slot, null);
        
        if (slot == SkillSlot.Passive)
        {
            TogglePassivePermanentEffects(false, removedSkill);
        }

        return removedSkill;
    }

    public Skill ReplaceSkill(int slotID, SkillSO otherSkillSO)
    {
        Skill replacedSkill = RemoveSkill(slotID, otherSkillSO.Slot);
        AddSkill(otherSkillSO, slotID);

        return replacedSkill;
    }

    public void TogglePassiveBattleEffects(bool enable)
    {
        foreach (Skill skill in _passiveSkills)
        {
            if (skill == null) { continue; }

            if (enable)
            {
                skill.TryExecute(Owner);
            }
            else
            {
                skill.Terminate(Owner);
            }
        }
    }

    public void ResetSkillCooldowns()
    {
        foreach (var active in _activeSkills)
        {
            if (active != null)
            {
                active.ResetCooldown();
            }
        }

        foreach (var special in _specialSkills)
        {
            if (special != null)
            {
                special.ResetCooldown();
            }
        }
    }

    private Dictionary<SkillSlot, Skill[]> CreateSkillSlots()
    {
        return new Dictionary<SkillSlot, Skill[]>()
        {
            { SkillSlot.Active, _activeSkills },
            { SkillSlot.Passive, _passiveSkills },
            { SkillSlot.Special, _specialSkills }
        };
    }

    private void AddSkill(SkillSO skillSO, int slotID)
    {
        Skill skill = new Skill(skillSO);

        if (skillSO.Slot == SkillSlot.Passive)
        {
            TogglePassivePermanentEffects(true, skill);
        }

        GetSkillSlots(skillSO.Slot)[slotID] = skill;
        OnSlotChange.Invoke(slotID, skillSO.Slot, skill);
    }

    private int FindFirstEmptySlot(SkillSlot slot)
    {
        Skill[] slots = GetSkillSlots(slot);

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                return i;
            }
        }

        return -1;
    }

    private void TogglePassivePermanentEffects(bool enable, Skill skill)
    {
        PassiveSkillSO passive = skill.SkillSO as PassiveSkillSO;

        if (enable)
        {
            passive.ApplyPermanentEffects(Owner, skill);
        }
        else
        {
            passive.RemovePermanentEffects(Owner, skill);
        }
    }
}