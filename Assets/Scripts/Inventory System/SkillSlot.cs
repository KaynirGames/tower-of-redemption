using System;
using UnityEngine;

/// <summary>
/// Слот умения в книге.
/// </summary>
[Serializable]
public class SkillSlot
{
    [SerializeField] private Skill _skill = null;
    [SerializeField] private SkillSlotType _slotType = SkillSlotType.ActiveSlot;
    [SerializeField] private int _slotID = 0;
    /// <summary>
    /// Умение в слоте книги.
    /// </summary>
    public Skill Skill => _skill;
    /// <summary>
    /// Тип слота умения в книге.
    /// </summary>
    public SkillSlotType SlotType => _slotType;
    /// <summary>
    /// Номер слота умения в книге.
    /// </summary>
    public int SlotID => _slotID;
    /// <summary>
    /// Для определения пустого слота.
    /// </summary>
    public bool IsEmpty => _skill == null;

    private bool _isCooldown = false;

    public SkillSlot(SkillSlotType slotType, int slotID)
    {
        _slotID = slotID;
        _slotType = slotType;
    }
    /// <summary>
    /// Вставить умение в слот.
    /// </summary>
    public void InsertSkill(Skill skill)
    {
        _skill = skill;
    }
    /// <summary>
    /// Убрать умение из слота.
    /// </summary>
    public void RemoveSkill()
    {
        _skill = null;
    }
}
