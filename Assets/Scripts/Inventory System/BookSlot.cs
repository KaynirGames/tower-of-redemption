using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Слот в книге умений.
/// </summary>
[Serializable]
public class BookSlot
{
    public delegate void OnSkillCooldownToggle(bool enable);
    public delegate void OnSkillCooldownUpdate(float timer);

    public OnSkillCooldownToggle OnCooldownToggle = delegate { };
    public OnSkillCooldownUpdate OnCooldownUpdate = delegate { };

    public Action OnRequiredEnergyShortage = delegate { };

    public BookSlotType SlotType { get; private set; }
    public Skill Skill { get; private set; }
    public bool IsCooldown { get; private set; }

    public bool IsEmpty => Skill == null;

    private Character _bookOwner;
    private float _cooldownTimer;

    public BookSlot(Character bookOwner, BookSlotType slotType)
    {
        SlotType = slotType;

        _bookOwner = bookOwner;
    }

    public void InsertSkill(Skill skill)
    {
        Skill = skill;
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
        OnCooldownToggle(true);

        _cooldownTimer = Skill.Cooldown;

        while (_cooldownTimer >= 0)
        {
            _cooldownTimer -= Time.deltaTime;
            OnCooldownUpdate(_cooldownTimer);

            yield return null;
        }

        IsCooldown = false;
        OnCooldownToggle(false);
    }
}
