using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Экземпляр умения персонажа.
/// </summary>
[Serializable]
public class SkillInstance
{
    public delegate void OnSkillCooldownTick(float timer);
    public delegate void OnSkillCooldownToggle(bool enable);

    public event OnSkillCooldownTick OnCooldownTick = delegate { };
    public event OnSkillCooldownToggle OnCooldownToggle = delegate { };

    public event Action OnRequiredEnergyShortage = delegate { };

    [SerializeField] private Skill _skill = null;

    public Skill Skill => _skill;

    private string _cachedDescription;

    private bool _isCooldown;
    private float _cooldownTimer;

    public SkillInstance(Skill skill)
    {
        _skill = skill;
    }

    public bool TryExecute(Character owner)
    {
        if (_skill.Slot == SkillSlot.Passive)
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
        _skill.Terminate(owner, owner.CurrentOpponent, this);
    }

    public string GetDescription()
    {
        if (_cachedDescription == null)
        {
            _cachedDescription = _skill.Description;
        }

        return _cachedDescription;
    }

    private bool ExecuteActiveSkill(Character owner)
    {
        if (_isCooldown) { return false; }

        if (!owner.Stats.IsEnoughEnergy(_skill.Cost))
        {
            OnRequiredEnergyShortage.Invoke();
            Debug.Log("Not enough energy!");
            return false;
        }

        _skill.Execute(owner, owner.CurrentOpponent, this);

        owner.StartCoroutine(CooldownRoutine());

        return true;
    }

    private void ExecutePassiveSkill(Character owner)
    {
        _skill.Execute(owner, owner.CurrentOpponent, this);
    }

    private IEnumerator CooldownRoutine()
    {
        OnCooldownToggle.Invoke(true);
        _isCooldown = true;

        _cooldownTimer = _skill.Cooldown;

        while (_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
            OnCooldownTick.Invoke(_cooldownTimer);

            yield return null;
        }

        OnCooldownToggle.Invoke(false);
        _isCooldown = false;
    }
}