using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Слот в книге умений.
/// </summary>
[Serializable]
public class BookSlot
{
    public Action<bool> OnSkillCooldownToggle = delegate { };
    public Action OnRequiredEnergyShortage = delegate { };

    public BookSlotType SlotType { get; private set; }
    public Skill Skill { get; private set; }
    public bool IsCooldown { get; private set; }

    public bool IsEmpty => Skill == null;

    private Character _bookOwner;

    private WaitForSecondsRealtime _cooldownWaitForSeconds;

    public BookSlot(Character bookOwner, BookSlotType slotType)
    {
        SlotType = slotType;

        _bookOwner = bookOwner;
    }

    public void InsertSkill(Skill skill)
    {
        Skill = skill;

        _cooldownWaitForSeconds = new WaitForSecondsRealtime(skill.Cooldown);
    }

    public Skill RemoveSkill()
    {
        Skill removedSkill = Skill;
        Skill = null;

        return removedSkill;
    }

    public void TryActivateSkill()
    {
        if (SlotType == BookSlotType.Passive) { return; }

        if (IsCooldown) { return; }

        if (!_bookOwner.Stats.IsEnoughEnergy(Skill.Cost))
        {
            OnRequiredEnergyShortage.Invoke();
            Debug.Log($"{Skill.SkillName}: недостаточно энергии!");
            return;
        }

        Character opponent = BattleManager.Instance.FindBattleOpponent(_bookOwner);
        Skill.Activate(_bookOwner, opponent);

        _bookOwner.StartCoroutine(SkillCooldownRoutine());
    }

    private IEnumerator SkillCooldownRoutine()
    {
        IsCooldown = true;
        OnSkillCooldownToggle.Invoke(true);
        yield return _cooldownWaitForSeconds;
        IsCooldown = false;
        OnSkillCooldownToggle.Invoke(false);
    }
}
