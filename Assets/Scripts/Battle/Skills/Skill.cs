using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Умение персонажа.
/// </summary>
[Serializable]
public class Skill
{
    public delegate void OnSkillCooldownTick(float timer);
    public delegate void OnSkillCooldownToggle(bool enable);

    public event OnSkillCooldownTick OnCooldownTick = delegate { };
    public event OnSkillCooldownToggle OnCooldownToggle = delegate { };

    public event Action OnRequiredEnergyShortage = delegate { };

    [SerializeField] private SkillObject _skillObject = null;

    public SkillObject SkillObject => _skillObject;

    private bool _isCooldown;
    private float _cooldownTimer;

    public Skill(SkillObject skillObject)
    {
        _skillObject = skillObject;
    }

    public bool TryExecute(Character owner)
    {
        if (_isCooldown) { return false; }

        if (!owner.Stats.IsEnoughEnergy(_skillObject.Cost))
        {
            OnRequiredEnergyShortage.Invoke();
            Debug.Log("Not enough energy!");
            return false;
        }

        _skillObject.Execute(owner, owner.CurrentOpponent);

        owner.StartCoroutine(CooldownRoutine());

        return true;
    }

    private IEnumerator CooldownRoutine()
    {
        OnCooldownToggle.Invoke(true);
        _isCooldown = true;

        _cooldownTimer = _skillObject.Cooldown;

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