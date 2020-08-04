using System;
using UnityEngine;

public class AbilityBook : MonoBehaviour
{
    /// <summary>
    /// Событие при отсутствии свободных слотов для умения.
    /// </summary>
    public static event Action<AbilityBook, Ability> OnAllSlotsOccupied = delegate { };

    [SerializeField] private int _activeMaxCount = 4; // Максимальное количество активных умений.
    [SerializeField] private int _passiveMaxCount = 3; // Максимальное количество пассивных умений.
    [SerializeField] private int _specialMaxCount = 1; // Максимальное количество особых умений.

    /// <summary>
    /// Активные умения.
    /// </summary>
    public Ability[] ActiveAbilities { get; private set; }
    /// <summary>
    /// Пассивные умения.
    /// </summary>
    public Ability[] PassiveAbilities { get; private set; }
    /// <summary>
    /// Особые умения.
    /// </summary>
    public Ability[] SpecialAbilities { get; private set; }

    private void Awake()
    {
        ActiveAbilities = new Ability[_activeMaxCount];
        PassiveAbilities = new Ability[_passiveMaxCount];
        SpecialAbilities = new Ability[_specialMaxCount];
    }
    /// <summary>
    /// Добавить умение.
    /// </summary>
    public void AddAbility(Ability ability)
    {
        int slot = -1;

        switch (ability.BookSlot)
        {
            case BookSlot.Active:
                {
                    slot = CheckSlotAvaliable(ActiveAbilities, _activeMaxCount);
                    if (slot >= 0) ActiveAbilities[slot] = ability;
                }
                break;
            case BookSlot.Passive:
                {
                    slot = CheckSlotAvaliable(PassiveAbilities, _passiveMaxCount);
                    if (slot >= 0) PassiveAbilities[slot] = ability;
                }
                break;
            case BookSlot.Special:
                {
                    slot = CheckSlotAvaliable(SpecialAbilities, _specialMaxCount);
                    if (slot >= 0) SpecialAbilities[slot] = ability;
                }
                break;
        }

        if (slot < 0) OnAllSlotsOccupied?.Invoke(this, ability);
    }
    /// <summary>
    /// Убрать умение из слота.
    /// </summary>
    public void RemoveAbility(int slot)
    {
        ChangeAbility(slot, null);
    }
    /// <summary>
    /// Поменять умение в слоте.
    /// </summary>
    public Ability ChangeAbility(int slot, Ability ability)
    {
        Ability oldAbility = null;

        switch (ability.BookSlot)
        {
            case BookSlot.Active:
                {
                    oldAbility = ActiveAbilities[slot];
                    ActiveAbilities[slot] = ability;
                }
                break;
            case BookSlot.Passive:
                {
                    oldAbility = ActiveAbilities[slot];
                    PassiveAbilities[slot] = ability;
                }
                break;
            case BookSlot.Special:
                {
                    oldAbility = ActiveAbilities[slot];
                    SpecialAbilities[slot] = ability;
                }
                break;
        }

        return oldAbility;
    }
    /// <summary>
    /// Проверить наличие свободного слота.
    /// </summary>
    private int CheckSlotAvaliable(Ability[] abilities, int maxSlots)
    {
        int slot = -1;

        if (abilities.Length < maxSlots)
        {
            slot = abilities.Length + 1;
        }

        return slot;
    }
}
