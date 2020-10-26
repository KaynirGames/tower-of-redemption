using UnityEngine;

/// <summary>
/// Основная анимация персонажа.
/// </summary>
[RequireComponent(typeof(Animator))]
public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Character _animatedCharacter = null;

    public Animator Animator { get; private set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public void PlayMoveClip(Vector2 moveDirection)
    {
        if (moveDirection != Vector2.zero)
        {
            Animator.SetFloat("Horizontal", moveDirection.x);
            Animator.SetFloat("Vertical", moveDirection.y);
        }
        Animator.SetFloat("Speed", moveDirection.sqrMagnitude);
    }

    public void PlayAttackClip()
    {
        Animator.SetTrigger("Attack");
    }

    public void PlayDefenceClip()
    {
        Animator.SetTrigger("Defence");
    }

    public void EnableMovementAfterAttackAnimation()
    {
        _animatedCharacter.ToggleMovement(true);
    }
}