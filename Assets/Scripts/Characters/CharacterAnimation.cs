using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Основная анимация персонажа.
/// </summary>
[RequireComponent(typeof(Animator))]
public partial class CharacterAnimation : MonoBehaviour
{
    private Animator _animator;
    private Dictionary<ClipType, string> _stateTags;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _stateTags = new Dictionary<ClipType, string>()
        {
            { ClipType.Move, "Move" },
            { ClipType.Attack, "Attack" },
            { ClipType.Hurt, "Hurt" },
            { ClipType.Death, "Death" }
        };
    }

    public void PlayMoveClip(Vector2 moveDirection)
    {
        if (moveDirection != Vector2.zero)
        {
            _animator.SetFloat("Horizontal", moveDirection.x);
            _animator.SetFloat("Vertical", moveDirection.y);
        }
        _animator.SetFloat("Speed", moveDirection.sqrMagnitude);
    }

    public void PlayAttackClip()
    {
        _animator.SetTrigger("Attack");
    }

    public void PlayHurtClip()
    {
        _animator.SetTrigger("Hurt");
    }

    public void PlayDeathClip()
    {
        _animator.SetTrigger("Death");
    }

    public float GetCurrentClipLength()
    {
        return _animator.GetCurrentAnimatorClipInfo(0).Length;
    }

    public bool IsClipDone(ClipType clipType)
    {
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        return stateInfo.IsTag(_stateTags[clipType]) && stateInfo.normalizedTime >= 1f;
    }
}