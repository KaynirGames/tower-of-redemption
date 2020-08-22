using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Книга умений.
/// </summary>
public class SkillBook : MonoBehaviour
{
    public delegate void OnSkillBookChange(int slotID, SkillSlotType slotType);
    /// <summary>
    /// Событие при любом изменении доступных слотов умений.
    /// </summary>
    public event OnSkillBookChange OnSkillSlotChange = delegate { };

    [SerializeField] private int _activeSlotsCount = 4; // Число слотов активных умений.
    [SerializeField] private int _passiveSlotsCount = 3; // Число слотов пассивных умений.
    [SerializeField] private int _specialSlotsCount = 1; // Число слотов особых умений.

    private SkillSlot[] _activeSlots; // Слоты активных умений.
    private SkillSlot[] _passiveSlots; // Слоты пассивных умений.
    private SkillSlot[] _specialSlots; // Слоты особых умений.

    private Dictionary<SkillSlotType, SkillSlot[]> _bookSlots; // Слоты книги.

    private void Awake()
    {
        _activeSlots = CreateSlots(_activeSlotsCount, SkillSlotType.ActiveSlot);
        _passiveSlots = CreateSlots(_passiveSlotsCount, SkillSlotType.PassiveSlot);
        _specialSlots = CreateSlots(_specialSlotsCount, SkillSlotType.SpecialSlot);

        _bookSlots = new Dictionary<SkillSlotType, SkillSlot[]>
        { 
            { SkillSlotType.ActiveSlot, _activeSlots },
            { SkillSlotType.PassiveSlot, _passiveSlots },
            { SkillSlotType.SpecialSlot, _specialSlots }
        };
    }
    /// <summary>
    /// Задать базовые умения для текущей специализации персонажа.
    /// </summary>
    public void SetBaseSkills(BaseStats currentSpec)
    {
        foreach (Skill skill in currentSpec.BaseSkills)
        {
            AddSkill(skill);
        }
    }
    /// <summary>
    /// Добавить умение в книгу.
    /// </summary>
    public void AddSkill(Skill skill)
    {
        int slotID = FindSlotAvaliable(skill.SkillSlotType);

        if (slotID >= 0)
        {
            InsertSkill(skill, slotID);
        }
        else
        {
            // Сообщаем, что все слоты заняты.
        }
    }
    /// <summary>
    /// Убрать умение из книги.
    /// </summary>
    public Skill RemoveSkill(int slotID, SkillSlotType slotType)
    {
        SkillSlot[] slots = GetSlots(slotType);

        Skill removedSkill = slots[slotID].Skill;
        slots[slotID].RemoveSkill();

        OnSkillSlotChange.Invoke(slotID, slotType);

        return removedSkill;
    }
    /// <summary>
    /// Заменить умение в книге.
    /// </summary>
    public Skill ChangeSkill(int slotID, Skill newSkill)
    {
        Skill removedSkill = RemoveSkill(slotID, newSkill.SkillSlotType);
        InsertSkill(newSkill, slotID);

        return removedSkill;
    }
    /// <summary>
    /// Получить набор слотов книги.
    /// </summary>
    public SkillSlot[] GetSlots(SkillSlotType slotType)
    {
        return _bookSlots[slotType];
    }
    /// <summary>
    /// Создать слоты книги.
    /// </summary>
    private SkillSlot[] CreateSlots(int slotAmount, SkillSlotType slotType)
    {
        SkillSlot[] slots = new SkillSlot[slotAmount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new SkillSlot(slotType, i);
        }

        return slots;
    }
    /// <summary>
    /// Вставить умение в слот книги.
    /// </summary>
    private void InsertSkill(Skill skill, int slotID)
    {
        SkillSlot[] slots = GetSlots(skill.SkillSlotType);
        slots[slotID].InsertSkill(skill);

        OnSkillSlotChange.Invoke(slotID, skill.SkillSlotType);
    }
    /// <summary>
    /// Найти первый свободный слот.
    /// </summary>
    private int FindSlotAvaliable(SkillSlotType slotType)
    {
        SkillSlot[] slots = GetSlots(slotType);

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
