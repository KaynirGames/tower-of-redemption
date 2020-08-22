using System.Collections.Generic;
using UnityEngine;

public class SkillDisplayUI : MonoBehaviour
{
    [Header("Отображение типов слотов:")]
    [SerializeField] private bool _displayActiveSlots = true;
    [SerializeField] private bool _displayPassiveSlots = true;
    [SerializeField] private bool _displaySpecialSlots = true;
    [Header("Объекты со слотами:")]
    [SerializeField] private GameObject _activeSlotsParent = null; // Объект с активными слотами.
    [SerializeField] private GameObject _passiveSlotsParent = null; // Объект с пассивными слотами.
    [SerializeField] private GameObject _specialSlotsParent = null; // Объект с особыми слотами.

    private SkillSlotUI[] _activeSlots = null; // Слоты активных умений.
    private SkillSlotUI[] _passiveSlots = null; // Слоты пассивных умений.
    private SkillSlotUI[] _specialSlots = null; // Слоты особых умений.

    private Dictionary<SkillSlotType, SkillSlotUI[]> _slotsDictionary; // Словарь слотов для отображения.

    private SkillBook _skillBook; // Текущая книга умений.

    private void Awake()
    {
        if (_displayActiveSlots)
        {
            _activeSlots = _activeSlotsParent.GetComponentsInChildren<SkillSlotUI>();
        }

        if (_displayPassiveSlots)
        {
            _passiveSlots = _passiveSlotsParent.GetComponentsInChildren<SkillSlotUI>();
        }

        if (_displaySpecialSlots)
        {
            _specialSlots = _specialSlotsParent.GetComponentsInChildren<SkillSlotUI>();
        }

        _slotsDictionary = new Dictionary<SkillSlotType, SkillSlotUI[]>
        { 
            { SkillSlotType.ActiveSlot, _activeSlots },
            { SkillSlotType.PassiveSlot, _passiveSlots },
            { SkillSlotType.SpecialSlot, _specialSlots }
        };
    }
    /// <summary>
    /// Инициализировать отображение умений персонажа.
    /// </summary>
    public void Init(SkillBook skillBook)
    {
        _skillBook = skillBook;

        if (_displayActiveSlots) { UpdateAllSlots(SkillSlotType.ActiveSlot); }
        if (_displayPassiveSlots) { UpdateAllSlots(SkillSlotType.PassiveSlot); }
        if (_displaySpecialSlots) { UpdateAllSlots(SkillSlotType.SpecialSlot); }

        _skillBook.OnSkillSlotChange += UpdateSkillSlot;
    }
    /// <summary>
    /// Обновить отображаемый слот умения.
    /// </summary>
    private void UpdateSkillSlot(int slotID, SkillSlotType slotType)
    {
        _slotsDictionary[slotType][slotID].UpdateSlotUI();
    }
    /// <summary>
    /// Обновить все отображаемые слоты умений.
    /// </summary>
    private void UpdateAllSlots(SkillSlotType slotType)
    {
        SkillSlot[] slots = _skillBook.GetSlots(slotType);
        SkillSlotUI[] slotsUI = _slotsDictionary[slotType];

        for (int i = 0; i < slots.Length; i++)
        {
            slotsUI[i].InitSlot(slots[i]);
        }
    }
}
