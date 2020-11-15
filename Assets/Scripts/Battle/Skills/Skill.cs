using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Skill
{
    public delegate void OnSkillCooldownTick(float timer);
    public delegate void OnSkillCooldownToggle(bool enable);

    public event OnSkillCooldownTick OnCooldownTick = delegate { };
    public event OnSkillCooldownToggle OnCooldownToggle = delegate { };

    public event Action OnRequiredEnergyShortage = delegate { };

    [SerializeField] private SkillSO _skillSO = null;

    public SkillSO SkillSO => _skillSO;

    public bool IsCooldown { get; private set; }

    private string _cachedDescription;
    private float _cooldownTimer;

    public Skill(SkillSO skillSO)
    {
        _skillSO = skillSO;
    }

    public bool TryExecute(Character owner)
    {
        if (owner.CurrentOpponent == null) { return false; }

        if (_skillSO.Slot == SkillSlot.Passive)
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

        _skillSO.Terminate(owner, owner.CurrentOpponent, this);
    }

    public string GetDescription()
    {
        if (_cachedDescription == null)
        {
            _cachedDescription = _skillSO.Description;
        }

        return _cachedDescription;
    }

    private bool ExecuteActiveSkill(Character owner)
    {
        if (IsCooldown) { return false; }

        if (!owner.Stats.IsEnoughEnergy(_skillSO.Cost))
        {
            OnRequiredEnergyShortage.Invoke();
            Debug.Log("Not enough energy!");
            return false;
        }

        _skillSO.Execute(owner, owner.CurrentOpponent, this);

        owner.StartCoroutine(CooldownRoutine());

        return true;
    }

    private void ExecutePassiveSkill(Character owner)
    {
        _skillSO.Execute(owner, owner.CurrentOpponent, this);
    }

    private IEnumerator CooldownRoutine()
    {
        OnCooldownToggle.Invoke(true);
        IsCooldown = true;

        _cooldownTimer = _skillSO.Cooldown;

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