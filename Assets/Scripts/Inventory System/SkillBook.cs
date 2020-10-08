using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Книга умений.
/// </summary>
public class SkillBook : MonoBehaviour
{
    public delegate void OnSkillSlotChange(int slotID, SkillSlot slot, Skill skill);

    public event OnSkillSlotChange OnSlotChange = delegate { };

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

    public void SetBaseSpecSkills(SpecBase currentSpec)
    {
        foreach (SkillObject skill in currentSpec.BaseSkills)
        {
            TryAddSkill(skill);
        }
    }

    public Skill[] GetSkillSlots(SkillSlot slot)
    {
        return _skillSlots[slot];
    }

    public bool TryAddSkill(SkillObject skillObject)
    {
        int slotID = FindFirstEmptySlot(skillObject.Slot);

        if (slotID >= 0)
        {
            AddSkill(skillObject, slotID);
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

        return removedSkill;
    }

    public Skill ReplaceSkill(int slotID, SkillObject newSkillObject)
    {
        Skill replacedSkill = RemoveSkill(slotID, newSkillObject.Slot);
        AddSkill(newSkillObject, slotID);

        return replacedSkill;
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

    private void AddSkill(SkillObject skillObject, int slotID)
    {
        Skill skill = new Skill(skillObject);

        GetSkillSlots(skillObject.Slot)[slotID] = skill;
        OnSlotChange.Invoke(slotID, skillObject.Slot, skill);
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
}