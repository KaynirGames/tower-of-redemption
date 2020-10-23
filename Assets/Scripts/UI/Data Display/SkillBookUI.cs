using System.Collections.Generic;
using UnityEngine;

public class SkillBookUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _slotsParents = null;

    private Dictionary<SkillSlot, SkillSlotUI[]> _skillSlotsUI;

    private SkillBook _skillBook;

    private void Awake()
    {
        _skillSlotsUI = CreateSlotsDictionary();
    }

    public void RegisterSkillBook(SkillBook skillBook)
    {
        _skillBook = skillBook;

        UpdateAllSlotsDisplay();

        _skillBook.OnSlotChange = UpdateSkillSlotDisplay;
    }

    public void ClearAllSlots()
    {
        foreach (SkillSlot key in _skillSlotsUI.Keys)
        {
            SkillSlotUI[] slotsUI = _skillSlotsUI[key];

            for (int i = 0; i < slotsUI.Length; i++)
            {
                slotsUI[i].UpdateSlotUI(null);
            }
        }
    }

    private SkillSlotUI[] CollectSkillSlotsUI(GameObject parent)
    {
        return parent != null
            ? parent.GetComponentsInChildren<SkillSlotUI>()
            : new SkillSlotUI[0];
    }

    private Dictionary<SkillSlot, SkillSlotUI[]> CreateSlotsDictionary()
    {
        var slotsDictionary = new Dictionary<SkillSlot, SkillSlotUI[]>();

        foreach (GameObject parent in _slotsParents)
        {
            SkillSlotUI[] slots = CollectSkillSlotsUI(parent);

            if (slots.Length > 0)
            {
                slotsDictionary.Add(slots[0].Slot, slots);
            }
        }

        return slotsDictionary;
    }

    private void UpdateSkillSlotDisplay(int slotID, SkillSlot slot, SkillInstance skill)
    {
        if (_skillSlotsUI.ContainsKey(slot))
        {
            _skillSlotsUI[slot][slotID].UpdateSlotUI(skill);
        }
    }

    private void UpdateAllSlotsDisplay()
    {
        foreach (SkillSlot key in _skillSlotsUI.Keys)
        {
            SkillInstance[] slots = _skillBook.GetSkillSlots(key);
            SkillSlotUI[] slotsUI = _skillSlotsUI[key];

            for (int i = 0; i < slots.Length; i++)
            {
                slotsUI[i].RegisterSlotUI(_skillBook.Owner);
                slotsUI[i].UpdateSlotUI(slots[i]);
            }
        }
    }
}
