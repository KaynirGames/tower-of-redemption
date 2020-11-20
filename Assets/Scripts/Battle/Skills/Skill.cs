using System;
using System.Collections;
using UnityEngine;

public class Skill
{
    public delegate void OnSkillCooldownTick(float timer);
    public delegate void OnSkillCooldownToggle(bool enable);

    public event OnSkillCooldownTick OnCooldownTick = delegate { };
    public event OnSkillCooldownToggle OnCooldownToggle = delegate { };

    public event Action OnRequiredEnergyShortage = delegate { };

    public SkillSO SkillSO { get; private set; }
    public bool IsCooldown { get; private set; }

    private string _cachedDescription;
    private float _cooldownTimer;

    public Skill(SkillSO skillSO)
    {
        SkillSO = skillSO;
    }

    public bool TryExecute(Character owner)
    {
        if (owner.CurrentOpponent == null) { return false; }

        if (SkillSO.Slot == SkillSlot.Passive)
        {
            ExecutePassiveSkill(owner);
            return true;
        }
        else
        {
            return ExecuteActiveSkill(owner);
        }
    }

    public void Terminate(Character owner)
    {
        if (owner.CurrentOpponent == null) { return; }

        SkillSO.Terminate(owner, owner.CurrentOpponent, this);
    }

    public string GetDescription()
    {
        if (_cachedDescription == null)
        {
            _cachedDescription = SkillSO.Description;
        }

        return _cachedDescription;
    }

    public void ResetCooldown()
    {
        if (IsCooldown)
        {
            _cooldownTimer = 0;
        }
    }

    private bool ExecuteActiveSkill(Character owner)
    {
        if (IsCooldown) { return false; }

        if (!owner.Stats.IsEnoughEnergy(SkillSO.Cost))
        {
            OnRequiredEnergyShortage.Invoke();
            Debug.Log("Not enough energy!");
            return false;
        }

        SkillSO.Execute(owner, owner.CurrentOpponent, this);

        owner.StartCoroutine(CooldownRoutine());

        return true;
    }

    private void ExecutePassiveSkill(Character owner)
    {
        SkillSO.Execute(owner, owner.CurrentOpponent, this);
    }

    private IEnumerator CooldownRoutine()
    {
        OnCooldownToggle.Invoke(true);
        IsCooldown = true;

        _cooldownTimer = SkillSO.Cooldown;

        while (_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
            OnCooldownTick.Invoke(_cooldownTimer);

            yield return null;
        }

        OnCooldownToggle.Invoke(false);
        IsCooldown = false;
    }
}