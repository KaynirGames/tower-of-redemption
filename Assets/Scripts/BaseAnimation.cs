using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaynirGames.Movement;

[RequireComponent(typeof(Animator))]
public class BaseAnimation : MonoBehaviour
{
    [SerializeField] private string _horizontalMoveParam = "Horizontal";
    [SerializeField] private string _verticalMoveParam = "Vertical";
    [SerializeField] private string _moveSpeedParam = "Speed";
    [SerializeField] private string _attackParam = "Attack";

    private Animator _animator;
    private CharacterMoveBase _characterMoveBase;
    private bool _canAttack = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterMoveBase = GetComponent<CharacterMoveBase>();
    }

    public void PlayMoveClip(Vector2 moveDirection)
    {
        if (moveDirection != Vector2.zero)
        {
            _animator.SetFloat(_horizontalMoveParam, moveDirection.x);
            _animator.SetFloat(_verticalMoveParam, moveDirection.y);
        }
        _animator.SetFloat(_moveSpeedParam, moveDirection.sqrMagnitude);
    }

    public void PlayAttackClip(float attackDelay, bool canMove)
    {
        if (_canAttack)
        {
            _animator.SetTrigger(_attackParam);
            StartCoroutine(AttackRoutine(attackDelay, canMove));
        }
    }

    private IEnumerator AttackRoutine(float attackDelay, bool canMove)
    {
        _canAttack = false;
        if (!canMove) _characterMoveBase.Disable();
        yield return new WaitForSeconds(attackDelay);
        _canAttack = true;
        if (!canMove) _characterMoveBase.Enable();
    }
}