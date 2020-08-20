using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Книга умений.
/// </summary>
public class SkillBook : MonoBehaviour
{
    /// <summary>
    /// Делегат для отслеживания изменений в книге умений.
    /// </summary>
    public delegate void OnSkillBookChange(Skill skill, int slotID);
    /// <summary>
    /// Событие при любом изменении доступных умений.
    /// </summary>
    public event OnSkillBookChange OnSkillChange = delegate { };

    [SerializeField] private int _activeSlotsCount = 4; // Число слотов активных умений.
    [SerializeField] private int _passiveSlotsCount = 3; // Число слотов пассивных умений.
    [SerializeField] private int _specialSlotsCount = 1; // Число слотов особых умений.

    private Skill[] _activeSkills; // Активные умения.
    private Skill[] _passiveSkills; // Пассивные умения.
    private Skill[] _specialSkills; // Особые умения.
    private Dictionary<SkillSlotType, Skill[]> _slotsDictionary; // Словарь слотов книги с набором умений.

    private void Awake()
    {
        _activeSkills = new Skill[_activeSlotsCount];
        _passiveSkills = new Skill[_passiveSlotsCount];
        _specialSkills = new Skill[_specialSlotsCount];

        _slotsDictionary = new Dictionary<SkillSlotType, Skill[]>
        { 
            { SkillSlotType.ActiveSlot, _activeSkills },
            { SkillSlotType.PassiveSlot, _passiveSkills },
            { SkillSlotType.SpecialSlot, _specialSkills }
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
        Skill[] slots = GetSkills(slotType);

        Skill removedSkill = slots[slotID];
        slots[slotID] = null;

        OnSkillChange.Invoke(null, slotID);

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
    /// Получить набор умений из слота книги.
    /// </summary>
    public Skill[] GetSkills(SkillSlotType slotType)
    {
        return _slotsDictionary[slotType];
    }
    /// <summary>
    /// Вставить умение в слот книги.
    /// </summary>
    private void InsertSkill(Skill skill, int slotID)
    {
        Skill[] slots = GetSkills(skill.SkillSlotType);
        slots[slotID] = skill;
        OnSkillChange.Invoke(skill, slotID);
    }
    /// <summary>
    /// Найти первый свободный слот.
    /// </summary>
    private int FindSlotAvaliable(SkillSlotType slotType)
    {
        Skill[] slots = GetSkills(slotType);

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
