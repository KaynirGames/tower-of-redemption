using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public CharacterStats Stats { get; protected set; }
    public CharacterEffects Effects { get; protected set; }
    public SkillBook SkillBook { get; protected set; }
    public CharacterAnimation Animations { get; protected set; }

    public Character CurrentOpponent { get; set; }

    protected virtual void Awake()
    {
        Stats = GetComponent<CharacterStats>();
        Effects = GetComponent<CharacterEffects>();
        SkillBook = GetComponent<SkillBook>();
        Animations = GetComponentInChildren<CharacterAnimation>();
    }

    public virtual void PrepareForBattle() { }

    public virtual void ExitBattle(Vector3 lastPosition)
    {
        transform.position = lastPosition;

        SkillBook.TogglePassiveBattleEffects(false);
        Effects.DisableBattleEffects();
    }

    protected abstract void Die();
}
