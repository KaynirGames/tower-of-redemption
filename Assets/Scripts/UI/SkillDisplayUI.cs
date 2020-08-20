using UnityEngine;

public class SkillDisplayUI : MonoBehaviour
{
    [SerializeField] private GameObject _activeSlotsParent = null; // Объект с активными слотами.
    [SerializeField] private GameObject _passiveSlotsParent = null; // Объект с пассивными слотами.
    [SerializeField] private GameObject _specialSlotsParent = null; // Объект с особыми слотами.

    private SkillSlotUI[] _activeSlots; // Слоты активных умений.
    private SkillSlotUI[] _passiveSlots; // Слоты пассивных умений.
    private SkillSlotUI[] _specialSlots; // Слоты особых умений.

    private SkillBook _skillBook; // Текущая книга умений.

    private void Awake()
    {
        _activeSlots = _activeSlotsParent.GetComponentsInChildren<SkillSlotUI>();
        _passiveSlots = _passiveSlotsParent.GetComponentsInChildren<SkillSlotUI>();
        _specialSlots = _specialSlotsParent.GetComponentsInChildren<SkillSlotUI>();
    }
    /// <summary>
    /// Инициализировать отображение умений персонажа.
    /// </summary>
    public void Init(SkillBook skillBook)
    {
        _skillBook = skillBook;

        UpdateAllSkills(SkillSlotType.ActiveSlot);
        UpdateAllSkills(SkillSlotType.PassiveSlot);
        UpdateAllSkills(SkillSlotType.SpecialSlot);

        _skillBook.OnSkillChange += UpdateSkillSlot;
    }
    /// <summary>
    /// Обновить отображаемый слот умения.
    /// </summary>
    private void UpdateSkillSlot(Skill skill, int slotID)
    {
        switch (skill.SkillSlotType)
        {
            case SkillSlotType.ActiveSlot:
                _activeSlots[slotID].SetSkill(skill);
                break;
            case SkillSlotType.PassiveSlot:
                _passiveSlots[slotID].SetSkill(skill);
                break;
            case SkillSlotType.SpecialSlot:
                _specialSlots[slotID].SetSkill(skill);
                break;
        }
    }
    /// <summary>
    /// Обновить отображаемые умения в типе слота.
    /// </summary>
    private void UpdateAllSkills(SkillSlotType slotType)
    {
        Skill[] skills = _skillBook.GetSkills(slotType);

        if (skills.Length > 0)
        {
            for (int i = 0; i < skills.Length; i++)
            {
                UpdateSkillSlot(skills[i], i);
            }
        }
    }
}
