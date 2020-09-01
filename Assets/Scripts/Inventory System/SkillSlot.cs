using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Слот умения в книге.
/// </summary>
[Serializable]
public class SkillSlot
{
    public Action<bool> OnSkillCooldownToggle = delegate { };
    public Action OnRequiredEnergyShortage = delegate { };

    public int SlotID { get; private set; }
    public SkillSlotType SlotType { get; private set; }
    public Skill Skill { get; private set; }

    public bool IsCooldown { get; private set; }

    public bool IsEmpty => Skill == null;

    private Character _skillOwner = null;

    public SkillSlot(SkillSlotType slotType, int slotID, Character skillOwner)
    {
        SlotID = slotID;
        SlotType = slotType;

        _skillOwner = skillOwner;
    }

    public void InsertSkill(Skill skill)
    {
        Skill = skill;
    }

    public void RemoveSkill()
    {
        Skill = null;
    }

    public void TryActivateSkill()
    {
        if (SlotType != SkillSlotType.PassiveSlot)
        {
            if (IsCooldown) { return; }

            if (!_skillOwner.Stats.IsEnoughEnergy(Skill.Cost))
            {
                OnRequiredEnergyShortage.Invoke();
                return;
            }

            Character opponent = BattleManager.Instance.FindTargetForSkillOwner(_skillOwner);
            Skill.Activate(_skillOwner, opponent);

            _skillOwner.StartCoroutine(SkillCooldownRoutine());
        }
    }

    private IEnumerator SkillCooldownRoutine()
    {
        IsCooldown = true;
        OnSkillCooldownToggle.Invoke(true);
        yield return new WaitForSecondsRealtime(Skill.Cooldown);
        IsCooldown = false;
        OnSkillCooldownToggle.Invoke(false);
    }
}
