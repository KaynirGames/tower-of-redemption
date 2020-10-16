using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Книга умений.
/// </summary>
public class SkillBook : MonoBehaviour
{
    public delegate void OnSkillSlotChange(int slotID, SkillSlot slot, SkillInstance skill);

    public OnSkillSlotChange OnSlotChange = delegate { };

    [SerializeField] private int _activeSkillsCount = 4;
    [SerializeField] private int _passiveSkillsCount = 3;
    [SerializeField] private int _specialSkillsCount = 1;

    public Character Owner { get; private set; }

    private SkillInstance[] _activeSkills;
    private SkillInstance[] _passiveSkills;
    private SkillInstance[] _specialSkills;

    private Dictionary<SkillSlot, SkillInstance[]> _skillSlots;

    private void Awake()
    {
        Owner = GetComponent<Character>();

        _activeSkills = new SkillInstance[_activeSkillsCount];
        _passiveSkills = new SkillInstance[_passiveSkillsCount];
        _specialSkills = new SkillInstance[_specialSkillsCount];

        _skillSlots = CreateSkillSlots();
    }

    public void SetBaseSpecSkills(SpecBase currentSpec)
    {
        foreach (Skill skill in currentSpec.BaseSkills)
        {
            TryAddSkill(skill);
        }
    }

    public SkillInstance[] GetSkillSlots(SkillSlot slot)
    {
        return _skillSlots[slot];
    }

    public bool TryAddSkill(Skill skill)
    {
        int slotID = FindFirstEmptySlot(skill.Slot);

        if (slotID >= 0)
        {
            AddSkill(skill, slotID);
            return true;
        }

        return false;
    }

    public SkillInstance RemoveSkill(int slotID, SkillSlot slot)
    {
        SkillInstance[] slots = GetSkillSlots(slot);

        SkillInstance removedSkill = slots[slotID];
        slots[slotID] = null;

        OnSlotChange.Invoke(slotID, slot, null);
        
        if (slot == SkillSlot.Passive)
        {
            TogglePassivePermanentEffects(false, removedSkill);
        }

        return removedSkill;
    }

    public SkillInstance ReplaceSkill(int slotID, Skill newSkill)
    {
        SkillInstance replacedSkill = RemoveSkill(slotID, newSkill.Slot);
        AddSkill(newSkill, slotID);

        return replacedSkill;
    }

    public void TogglePassiveBattleEffects(bool enable)
    {
        foreach (SkillInstance instance in _passiveSkills)
        {
            if (instance == null) { continue; }

            if (enable)
            {
                instance.TryExecute(Owner);
            }
            else
            {
                instance.Terminate(Owner);
            }
        }
    }

    private Dictionary<SkillSlot, SkillInstance[]> CreateSkillSlots()
    {
        return new Dictionary<SkillSlot, SkillInstance[]>()
        {
            { SkillSlot.Active, _activeSkills },
            { SkillSlot.Passive, _passiveSkills },
            { SkillSlot.Special, _specialSkills }
        };
    }

    private void AddSkill(Skill skill, int slotID)
    {
        SkillInstance instance = new SkillInstance(skill);

        if (skill.Slot == SkillSlot.Passive)
        {
            TogglePassivePermanentEffects(true, instance);
        }

        GetSkillSlots(skill.Slot)[slotID] = instance;
        OnSlotChange.Invoke(slotID, skill.Slot, instance);
    }

    private int FindFirstEmptySlot(SkillSlot slot)
    {
        SkillInstance[] slots = GetSkillSlots(slot);

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                return i;
            }
        }

        return -1;
    }

    private void TogglePassivePermanentEffects(bool enable, SkillInstance skillInstance)
    {
        PassiveSkill passive = skillInstance.Skill as PassiveSkill;

        if (enable)
        {
            passive.ApplyPermanentEffects(Owner, skillInstance);
        }
        else
        {
            passive.RemovePermanentEffects(Owner, skillInstance);
        }
    }
}