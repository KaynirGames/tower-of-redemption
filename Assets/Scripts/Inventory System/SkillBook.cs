using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Книга умений.
/// </summary>
public class SkillBook : MonoBehaviour
{
    public delegate void OnSkillBookSlotChanged(int slotID, BookSlotType slotType);

    public event OnSkillBookSlotChanged OnSkillSlotChanged = delegate { };

    [SerializeField] private int _activeSlotCount = 4;
    [SerializeField] private int _passiveSlotCount = 3;
    [SerializeField] private int _specialSlotCount = 1;

    private BookSlot[] _activeSlots;
    private BookSlot[] _passiveSlots;
    private BookSlot[] _specialSlots;

    private Dictionary<BookSlotType, BookSlot[]> _bookSlots;

    private Character _bookOwner;

    private void Awake()
    {
        _bookOwner = GetComponent<Character>();

        _activeSlots = CreateBookSlots(_activeSlotCount, BookSlotType.Active);
        _passiveSlots = CreateBookSlots(_passiveSlotCount, BookSlotType.Passive);
        _specialSlots = CreateBookSlots(_specialSlotCount, BookSlotType.Special);

        _bookSlots = CreateBookSlotDictionary();
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
        Skill newSkill = Instantiate(skill);

        int slotID = FindFirstEmptySlot(skill.SlotType);

        if (slotID >= 0)
        {
            InsertSkill(newSkill, slotID);
        }
        else
        {
            // Вызвать меню для замены умения в слоте.
        }
    }

    public Skill RemoveSkill(int slotID, BookSlotType slotType)
    {
        BookSlot[] slots = GetBookSlots(slotType);

        Skill removedSkill = slots[slotID].RemoveSkill();

        OnSkillSlotChanged.Invoke(slotID, slotType);

        if (slotType == BookSlotType.Passive)
        {
            removedSkill.Deactivate(_bookOwner, null);
        }

        return removedSkill;
    }

    public Skill ReplaceSkill(int slotID, Skill newSkill)
    {
        Skill replacedSkill = RemoveSkill(slotID, newSkill.SlotType);

        InsertSkill(newSkill, slotID);

        return replacedSkill;
    }

    public BookSlot[] GetBookSlots(BookSlotType slotType)
    {
        return _bookSlots[slotType];
    }

    private BookSlot[] CreateBookSlots(int slotAmount, BookSlotType slotType)
    {
        BookSlot[] slots = new BookSlot[slotAmount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new BookSlot(_bookOwner, slotType);
        }

        return slots;
    }

    private Dictionary<BookSlotType, BookSlot[]> CreateBookSlotDictionary()
    {
        return new Dictionary<BookSlotType, BookSlot[]>()
        {
            { BookSlotType.Active, _activeSlots },
            { BookSlotType.Passive, _passiveSlots },
            { BookSlotType.Special, _specialSlots }
        };
    }

    private void InsertSkill(Skill skill, int slotID)
    {
        GetBookSlots(skill.SlotType)[slotID].InsertSkill(skill);

        OnSkillSlotChanged.Invoke(slotID, skill.SlotType);

        if (skill.SlotType == BookSlotType.Passive)
        {
            skill.Activate(_bookOwner, null);
        }
    }

    private int FindFirstEmptySlot(BookSlotType slotType)
    {
        BookSlot[] slots = GetBookSlots(slotType);

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsEmpty)
            {
                return i;
            }
        }

        return -1;
    }
}