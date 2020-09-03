using System.Collections.Generic;
using UnityEngine;

public class SkillBookUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _slotsParents = null;

    private Dictionary<BookSlotType, BookSlotUI[]> _slotsDictionary;

    private SkillBook _skillBook;

    private void Awake()
    {
        _slotsDictionary = CreateSlotsDictionary();
    }

    public void Init(SkillBook skillBook)
    {
        _skillBook = skillBook;

        UpdateAllSlots(BookSlotType.Active);
        UpdateAllSlots(BookSlotType.Passive);
        UpdateAllSlots(BookSlotType.Special);

        _skillBook.OnSkillSlotChanged += UpdateSkillSlot;
    }

    private BookSlotUI[] CollectBookSlotsUI(GameObject parent)
    {
        return parent != null
            ? parent.GetComponentsInChildren<BookSlotUI>()
            : new BookSlotUI[0];
    }

    private Dictionary<BookSlotType, BookSlotUI[]> CreateSlotsDictionary()
    {
        var slotsDictionary = new Dictionary<BookSlotType, BookSlotUI[]>();

        foreach (GameObject parent in _slotsParents)
        {
            BookSlotUI[] slots = CollectBookSlotsUI(parent);

            if (slots.Length > 0)
            {
                slotsDictionary.Add(slots[0].SlotType, slots);
            }
        }

        return slotsDictionary;
    }

    private void UpdateSkillSlot(int slotID, BookSlotType slotType)
    {
        if (_slotsDictionary.ContainsKey(slotType))
        {
            _slotsDictionary[slotType][slotID].UpdateSlotDisplayUI();
        }
    }

    private void UpdateAllSlots(BookSlotType slotType)
    {
        if (_slotsDictionary.ContainsKey(slotType))
        {
            BookSlot[] slots = _skillBook.GetBookSlots(slotType);
            BookSlotUI[] slotsUI = _slotsDictionary[slotType];

            for (int i = 0; i < slots.Length; i++)
            {
                slotsUI[i].InitSlot(slots[i]);
            }
        }
    }
}
