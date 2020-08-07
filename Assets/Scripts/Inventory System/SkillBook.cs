﻿using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Книга умений.
/// </summary>
public class SkillBook : MonoBehaviour
{
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
        Skill[] slots = GetSkills(skill.SkillSlotType);
        int slotID = FindSlotAvaliable(skill.SkillSlotType);

        if (slotID >= 0)
        {
            slots[slotID] = skill;
            // Сообщаем UI.
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

        // Сообщаем UI.

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