using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public CharacterStats Stats { get; protected set; }
    public CharacterEffects Effects { get; protected set; }
    public SkillBook SkillBook { get; protected set; }

    public Character CurrentOpponent { get; set; }

    protected virtual void Awake()
    {
        Stats = GetComponent<CharacterStats>();
        Effects = GetComponent<CharacterEffects>();
        SkillBook = GetComponent<SkillBook>();
    }

    public virtual void ToggleMovement(bool enable) { }

    public virtual void PrepareForBattle() { }

    protected abstract void Die();
}
