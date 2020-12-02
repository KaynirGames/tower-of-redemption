using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public CharacterStats Stats { get; protected set; }
    public CharacterEffects Effects { get; protected set; }
    public SkillBook SkillBook { get; protected set; }
    public AnimationController Animations { get; protected set; }

    public Character CurrentOpponent { get; set; }

    protected virtual void Awake()
    {
        Stats = GetComponent<CharacterStats>();
        Effects = GetComponent<CharacterEffects>();
        SkillBook = GetComponent<SkillBook>();
        Animations = GetComponentInChildren<AnimationController>();
    }

    public virtual void PrepareForBattle() { }

    public virtual void ExitBattle(Vector3 lastPosition)
    {
        transform.position = lastPosition;

        SkillBook.TogglePassiveBattleEffects(false);
    }

    protected abstract void Die();

    protected void PlayMoveAnimation(Vector2 moveDirection)
    {
        if (moveDirection != Vector2.zero)
        {
            Animations.SetParameter("Horizontal", moveDirection.x);
            Animations.SetParameter("Vertical", moveDirection.y);
        }

        Animations.SetParameter("Speed", moveDirection.sqrMagnitude);
    }
}
