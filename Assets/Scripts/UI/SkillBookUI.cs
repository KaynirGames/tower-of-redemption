using UnityEngine;
using UnityEngine.UI;

public class SkillBookUI : MonoBehaviour
{
    [SerializeField] private Image _playerSpecIcon = null;
    [SerializeField] private GameObject _activeSlotsParent = null;
    [SerializeField] private GameObject _passiveSlotsParent = null;
    [SerializeField] private GameObject _specialSlotsParent = null;

    private SkillBook _skillBook;
    private PlayerSpec _playerSpec;
    private CharacterStats _playerStats;

    private SkillSlotUI[] _activeSlots;
    private SkillSlotUI[] _passiveSlots;
    private SkillSlotUI[] _specialSlots;

    private void Awake()
    {
        _activeSlots = _activeSlotsParent.GetComponentsInChildren<SkillSlotUI>();
        _passiveSlots = _passiveSlotsParent.GetComponentsInChildren<SkillSlotUI>();
        _specialSlots = _specialSlotsParent.GetComponentsInChildren<SkillSlotUI>();

        Player.OnPlayerActive += Init;
    }

    private void Init(Player player)
    {
        _skillBook = player.SkillBook;
        _playerSpec = player.PlayerSpec;
        _playerStats = player.PlayerStats;

        _skillBook.OnSkillChange += UpdateSkillSlot;

        _playerSpecIcon.sprite = _playerSpec.Icon;
        _playerSpecIcon.gameObject.SetActive(true);
    }
    /// <summary>
    /// Обновить слот умения.
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
}
